using UnityEngine;

namespace com.ggj2026teamname.gamename.Interactables
{
    public abstract class Interactable_Base : MonoBehaviour
    {
        [SerializeField] private GameObject _activeObjectIndicator;
        public abstract void Interact();
                    
        public virtual void OnInteractAreaEntered()
        {
            _activeObjectIndicator.SetActive(true);
        }

        public virtual void OnInteractAreaExited()
        {
            _activeObjectIndicator.SetActive(false);
        }
                        
        protected void Awake()
        {
            _activeObjectIndicator.SetActive(false);
        }
    }
}