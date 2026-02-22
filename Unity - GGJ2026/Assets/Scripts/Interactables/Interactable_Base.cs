using R3;
using UnityEngine;

namespace com.ggj2026teamname.gamename.Interactables
{
    public abstract class Interactable_Base : MonoBehaviour
    {
        [SerializeField] private GameObject _activeObjectIndicator;
        public abstract void Interact();

        public ReadOnlyReactiveProperty<bool> PlayerIsInInteractionZone => _playerIsInInteractionZone;
        private readonly ReactiveProperty<bool> _playerIsInInteractionZone = new(false);
        
        public virtual void OnInteractAreaEntered()
        {
            _playerIsInInteractionZone.Value = true;

            if (_activeObjectIndicator)
            {
                _activeObjectIndicator.SetActive(true);
            }
        }

        public virtual void OnInteractAreaExited()
        {
            _playerIsInInteractionZone.Value = false;

            if (_activeObjectIndicator)
            {
                _activeObjectIndicator.SetActive(false);
            }
        }
                        
        protected void Awake()
        {
            if (_activeObjectIndicator)
            {
                _activeObjectIndicator.SetActive(false);
            }
        }
    }
}