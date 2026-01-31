using com.ggj2026teamname.gamename.Interactables;
using UnityEngine;

namespace com.ggj2026teamname.gamename
{
    public class PlayerInteractHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput_Base _playerInput;
        private Interactable_Base _interactable;

        private void OnEnable()
        {
            _playerInput.RegisterInteractAction(Interact);
        }

        private void OnDisable()
        {
            _playerInput.DeregisterInteractAction(Interact);
        }

        public void Interact()
        {
            if (!_interactable)
            {
                return;
            }
            _interactable.Interact();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Interactable_Base>(out _interactable))
            {
                _interactable.OnInteractAreaEntered();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Interactable_Base>(out var interactable) && interactable == _interactable)
            {
                _interactable.OnInteractAreaExited();
                _interactable = null;
            }   
        }
    }
}