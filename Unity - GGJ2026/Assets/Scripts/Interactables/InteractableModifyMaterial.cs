using System;
using System.Collections.Generic;
using com.ggj2026teamname.gamename.Interactables;
using R3;
using UnityEngine;

namespace com.ggj2026teamname.gamename
{
    public class InteractableModifyMaterial : MonoBehaviour
    {
        public Interactable_Base interactable;
        
        [SerializeField] private List<SpriteRenderer> sprites;
        
        [SerializeField] private Material isInteractableMaterial;
        [SerializeField] private Material notInteractableMaterial;
        
        private void Awake()
        {
            if (sprites is { Count: > 0 })
            {
                interactable.PlayerIsInInteractionZone
                    .TakeUntil(destroyCancellationToken)
                    .Subscribe(isInteractable =>
                    {
                        foreach (var o in sprites)
                        {
                            o.sharedMaterial = isInteractable ? isInteractableMaterial : notInteractableMaterial;
                        }
                    });
            }
        }
    }
}
