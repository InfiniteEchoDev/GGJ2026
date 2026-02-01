using UnityEngine;
using Yarn.Unity;

namespace com.ggj2026teamname.gamename.Interactables
{
    public class Interactable_Dialogue : Interactable_Base
    {
        [SerializeField] private string _nodeName;
        [SerializeField] private DialogueRunner _dialogueRunner;



        public override void Interact()
        {
            if (_dialogueRunner.IsDialogueRunning)
            {
                return;
            }
            
            _dialogueRunner.StartDialogue(_nodeName); 
        }


    }
}