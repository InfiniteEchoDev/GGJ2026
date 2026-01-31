using UnityEngine;

namespace com.ggj2026teamname.gamename.Interactables
{
    public abstract class Interactable_Base : MonoBehaviour
    {
        public abstract void Interact();
        public abstract void OnInteractAreaEntered();
        public abstract void OnInteractAreaExited();
    }
}