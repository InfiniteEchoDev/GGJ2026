using System;
using System.Collections.Generic;
using R3;
using R3.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Unit = R3.Unit;

namespace com.ggj2026teamname.gamename
{
    public class PixelPerfectUIController : MonoBehaviour
    {
        public PixelPerfectCamera pixelPerfectCamera;

        public List<Canvas> pixelPerfectCanvases;
        public List<RectTransform> positionRects = new();
        public List<RawImage> renderTextureOutputs = new();
        
        public void Awake()
        {
            if (pixelPerfectCamera)
            {
                var obsScreenSize = Observable
                    .EveryUpdate(UnityFrameProvider.EarlyUpdate)
                    .Prepend(Unit.Default)
                    .Select(_ => new Vector2Int(Screen.width, Screen.height))
                    .DistinctUntilChanged();
                
                var obsPixelArtSize = Observable
                    .EveryUpdate(UnityFrameProvider.EarlyUpdate)
                    .Prepend(Unit.Default)
                    .Select(_ => new Vector2Int(pixelPerfectCamera.refResolutionX, pixelPerfectCamera.refResolutionY))
                    .DistinctUntilChanged();
                
                var pixelCamera = pixelPerfectCamera.GetComponent<Camera>();

                foreach (var canvas in pixelPerfectCanvases)
                {
                    if(!canvas) continue;
                    
                    var canvasScaler = canvas.GetComponent<CanvasScaler>();
                    if(!canvasScaler) continue;

                    obsScreenSize
                        .CombineLatest(obsPixelArtSize, (screenSize, pixelArtSize) => (parentSize: screenSize, pixelArtSize))
                        .TakeUntil(destroyCancellationToken)
                        .Subscribe(t =>
                        {
                            var (screenSize, pixelArtSize) = t;
                            
                            ComputeSubcontainerSize(pixelArtSize, screenSize, out var pixelRatio);
                            
                            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
                            canvasScaler.scaleFactor = pixelRatio;
                            canvasScaler.referencePixelsPerUnit = 1;
                        });
                }
                
                foreach (var positionRect in positionRects)
                {
                    var parent = positionRect.parent.GetComponent<RectTransform>();
                    var rootCanvas = parent.GetComponent<Canvas>()?.rootCanvas;
                    if(!parent || !rootCanvas) continue;
                    
                    parent.OnRectTransformDimensionsChangeAsObservable()
                        .Prepend(Unit.Default)
                        .Select(_ => parent.rect)
                        .DistinctUntilChanged()
                        .CombineLatest(obsPixelArtSize, (parentSize, pixelArtSize) => (parentSize, pixelArtSize))
                        .TakeUntil(destroyCancellationToken)
                        .Subscribe(t =>
                        {
                            var (parentRect, pixelArtSize) = t;
                            
                            var subcontainerSize = ComputeSubcontainerSize(pixelArtSize, parentRect.size, out _);
                            
                            positionRect.anchorMin = positionRect.anchorMax = positionRect.pivot = new Vector2(0.5f, 0.5f);
                            positionRect.anchoredPosition = Vector2.zero;
                            positionRect.sizeDelta = subcontainerSize;
                        });
                    
                    // this code offsets the transform after Canvas' complicated layout shit has run in order
                    // to align it as closely as possible with our camera's screen space pixel grid!
                    Observable.FromEvent(h => new Canvas.WillRenderCanvases(h),
                            e => Canvas.willRenderCanvases += e,
                            e => Canvas.willRenderCanvases -= e,
                            destroyCancellationToken)
                        .Subscribe(_ =>
                        {
                            Vector3 world = positionRect.position; // layout result

                            Vector3 screen = rootCanvas.worldCamera.WorldToScreenPoint(world);

                            Vector3 rounded = new Vector3(
                                Mathf.Round(screen.x),
                                Mathf.Round(screen.y),
                                screen.z
                            );

                            Vector3 snappedWorld = rootCanvas.worldCamera.ScreenToWorldPoint(rounded);

                            Vector3 correction = snappedWorld - world;

                            positionRect.position += correction;
                        });

                }

                ulong rtCounter = 0;
                RenderTexture renderTexture = null;
                obsPixelArtSize
                    .TakeUntil(destroyCancellationToken)
                    .Subscribe(pixelArtSize =>
                    {
                        var descriptor = new RenderTextureDescriptor(pixelArtSize.x, pixelArtSize.y, RenderTextureFormat.ARGB32, 32);
                        
                        // clear out the previous texture, if there is one
                        renderTexture?.Release();
                        
                        // allocate a new texture at the new pixel art size
                        renderTexture = new RenderTexture(descriptor)
                        {
                            name = $"{nameof(PixelPerfectUIController)}_RenderTexture_{rtCounter}",
                            filterMode = FilterMode.Point
                        };
                        ++rtCounter;

                        pixelCamera.targetTexture = renderTexture;
                        
                        foreach (var o in renderTextureOutputs)
                        {
                            o.texture = renderTexture;
                        }
                    });

                destroyCancellationToken.Register(() =>
                {
                    renderTexture.Release();
                });

                return;

                Vector2 ComputeSubcontainerSize(Vector2Int pixelArtSize, Vector2 containerSize, out int pixelRatio)
                {
                    pixelRatio = 1;
                    
                    if (containerSize.x < pixelArtSize.x ||
                        containerSize.y < pixelArtSize.y)
                    {
                        return containerSize;
                    }
                    
                    pixelRatio = Mathf.Min(
                        Mathf.FloorToInt(containerSize.x / pixelArtSize.x),
                        Mathf.FloorToInt(containerSize.y / pixelArtSize.y));

                    return pixelArtSize * pixelRatio;
                }
            }
        }
    }
}
