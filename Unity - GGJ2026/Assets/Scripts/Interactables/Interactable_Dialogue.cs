using System;
using Pixelplacement;
using UnityEngine;
using Yarn.Unity;

namespace com.ggj2026teamname.gamename.Interactables
{
    public class Interactable_Dialogue : Interactable_Base
    {
        [SerializeField] private string _nodeName;
        [SerializeField] private DialogueRunner _dialogueRunner;
        [SerializeField] private GameObject _activeObjectIndicator;

        private void Awake()
        {
            _activeObjectIndicator.SetActive(false);
        }

        public override void Interact()
        {
            if (_dialogueRunner.IsDialogueRunning)
            {
                return;
            }
            
            _dialogueRunner.StartDialogue(_nodeName); 
        }

        public override void OnInteractAreaEntered()
        {
            _activeObjectIndicator.SetActive(true);
        }

        public override void OnInteractAreaExited()
        {
            _activeObjectIndicator.SetActive(false);
        }
    }
}