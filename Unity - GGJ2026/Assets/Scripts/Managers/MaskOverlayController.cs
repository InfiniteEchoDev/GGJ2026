using UnityEngine;
using UnityEngine.Serialization;
using Yarn.Unity;

namespace com.ggj2026teamname.gamename
{
    public class MaskOverlayController : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Animator _maskOverlayAnimator;
        [SerializeField] private DialogueRunner _dialogueRunner;
        [SerializeField] private PlayerInput_NewInputSystem _player;
        
        [Header("Scene Start")]
        [SerializeField] private string _sceneStartDialogueNode;
        
        [Header("Scene End")]
        [SerializeField] private string _sceneEndDialogueNode;
        [SerializeField] private GameScene _sceneToSwitchTo;
        
        [Header("Debug")]
        [SerializeField] private bool _skipStartAnimation = false;
        
        void Start()
        {
            if (_skipStartAnimation)
            {
                return;
            }
            
            _maskOverlayAnimator.SetTrigger("SceneStartFadeIn");
            
            // This is the wrong way to do this but we're in a jam! --mrs
            if (_dialogueRunner is null)
            {
                _dialogueRunner = FindFirstObjectByType<DialogueRunner>();
            }

            if (_player is null)
            {
                _player = FindFirstObjectByType<PlayerInput_NewInputSystem>();
            }
        }

        public void OnSceneStartFadeIn()
        {
            _dialogueRunner.onDialogueComplete.AddListener(OnStartDialogueComplete);
            _dialogueRunner.StartDialogue(_sceneStartDialogueNode);
            _player.SetPlayerInputState(false);
        }

        private void OnStartDialogueComplete()
        {
            _dialogueRunner.onDialogueComplete.RemoveListener(OnStartDialogueComplete);
            _maskOverlayAnimator.SetTrigger("SceneStartFadeOut");
        }

        public void TriggerFadeOutForSceneChange()
        {
            _maskOverlayAnimator.SetTrigger("SceneEndFadeIn");
        }
        public void MainMenuFadeOutForSceneChange()
        {
            _maskOverlayAnimator.SetTrigger("SceneEndFadeOut");
        }

        public void OnSceneEndFadeIn()
        {
            _dialogueRunner.onDialogueComplete.AddListener(OnEndDialogueComplete);
            _dialogueRunner.StartDialogue(_sceneEndDialogueNode);
        }
        
        private void OnEndDialogueComplete()
        {
            _dialogueRunner.onDialogueComplete.RemoveListener(OnEndDialogueComplete);
            _maskOverlayAnimator.SetTrigger("SceneEndFadeOut");
            _player.SetPlayerInputState(false);
        }
        
        public void OnFadeOutChangeScene()
        {
            Debug.Log("SCENE CHANGE!");
            GameManager.Instance.SwitchToScene(_sceneToSwitchTo);
        }
    }
}
