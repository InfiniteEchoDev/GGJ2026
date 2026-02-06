using System;
using System.Collections.Generic;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace com.ggj2026teamname.gamename
{
    public class ChapterSceneDirector : MonoBehaviour
    {
        public Camera uiCamera;
        public Camera playerCamera;
        public Player player;
        public PixelPerfectCamera pixelPerfectCamera;
        public List<RawImage> renderTextureOutputs = new();
        
        public void Initialize()
        {
            if (uiCamera && pixelPerfectCamera)
            {
                var obsScreenSize = Observable
                    .EveryUpdate(UnityFrameProvider.EarlyUpdate)
                    .Select(_ => new Vector2Int(Screen.width, Screen.height))
                    .DistinctUntilChanged();
                
                var obsRenderTextureSize = Observable
                    .EveryUpdate(UnityFrameProvider.Update)
                    .Select(_ =>
                    {
                        return new Vector2Int(pixelPerfectCamera.refResolutionX,
                            pixelPerfectCamera.refResolutionY);
                    })
                    .DistinctUntilChanged();
                
                
                obsScreenSize
                    .TakeUntil(destroyCancellationToken)
                    .CombineLatest(obsRenderTextureSize, (screenSize, contentSize) => (screenSize, contentSize))
                    .Subscribe(t =>
                    {
                        var subcontainerSize = ComputeSubcontainerSize(t.contentSize, t.screenSize);
                        var bottomLeftOffset = (t.screenSize - subcontainerSize) / 2;
                        uiCamera.pixelRect = new Rect(bottomLeftOffset, subcontainerSize);
                    });
                
                var pixelCamera = pixelPerfectCamera.GetComponent<Camera>();

                RenderTexture renderTexture = null;
                obsRenderTextureSize
                    .TakeUntil(destroyCancellationToken)
                    .Subscribe(size =>
                    {
                        var descriptor = new RenderTextureDescriptor(size.x, size.y, RenderTextureFormat.ARGB32, 32);
                        
                        renderTexture?.Release();
                        renderTexture = new RenderTexture(descriptor)
                        {
                            filterMode = FilterMode.Point
                        };

                        pixelCamera.targetTexture = renderTexture;
                        
                        foreach (var o in renderTextureOutputs)
                        {
                            o.texture = renderTexture;
                        }
                    });

                if (player && playerCamera)
                {
                    Observable.EveryUpdate(UnityFrameProvider.PreLateUpdate)
                        .TakeUntil(destroyCancellationToken)
                        .Subscribe(pixelRatio =>
                        {
                            playerCamera.transform.position = new Vector3(player.transform.position.x,
                                player.transform.position.y,
                                playerCamera.transform.position.z);
                        });
                }


                destroyCancellationToken.Register(() =>
                {
                    renderTexture.Release();
                });

                return;

                Vector2Int ComputeSubcontainerSize(Vector2Int contentSize, Vector2Int containerSize)
                {
                    if (containerSize.x < contentSize.x ||
                        containerSize.y < contentSize.y)
                    {
                        return containerSize;
                    }
                    
                    var pixelRatio = Mathf.Min(
                        containerSize.x / contentSize.x,
                        containerSize.y / contentSize.y);

                    return contentSize * pixelRatio;
                }
            }
        }
    }
}
