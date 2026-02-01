using com.ggj2026teamname.gamename.Interactables;
using UnityEngine;
using UnityEngine.Events;

namespace com.ggj2026teamname.gamename
{
    public class Interactable_Item : Interactable_Base
    {
        [SerializeField] private UnityEvent _onInteractionEvent;
        [SerializeField] private bool _deactivateOnInteract;
        
        public override void Interact()
        {
            _onInteractionEvent.Invoke();
            if (_deactivateOnInteract)
            {
                gameObject.SetActive(false);
            }
        }

    }
}
