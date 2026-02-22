using System;
using R3;
using UnityEngine;

namespace com.ggj2026teamname.gamename
{
    public class Player : MonoBehaviour
    {
        private static readonly int IsFacingRight = Animator.StringToHash("IsFacingRight");
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        
        private LocalSceneManager _localSceneManager;
        
        [SerializeField] private PlayerInput_NewInputSystem inputSystem;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private Animator animationController;
        
        private void Awake()
        {
            _localSceneManager = FindFirstObjectByType<LocalSceneManager>();
        }

        public void Begin()
        {
            _localSceneManager.IsGlobalAnimationRunning
                .CombineLatest(_localSceneManager.IsDialogueRunning,
                    (animating, dialogueRunning) => !animating && !dialogueRunning)
                .TakeUntil(destroyCancellationToken)
                .Subscribe(canUseInput =>
                {
                    inputSystem.SetPlayerInputState(canUseInput);
                });
            
            Observable.EveryUpdate(UnityFrameProvider.PreLateUpdate)
                .TakeUntil(destroyCancellationToken)
                .Subscribe(pixelRatio =>
                {
                    _localSceneManager.PlayerCamera.transform.position = new Vector3(transform.position.x,
                        transform.position.y,
                        _localSceneManager.PlayerCamera.transform.position.z);
                });

            if (animationController && playerMover)
            {
                playerMover.MovementState
                    .TakeUntil(destroyCancellationToken)
                    .Subscribe(state =>
                    {
                        //animationController.SetBool(IsFacingRight, state.IsFacingRight);
                        animationController.SetBool(IsWalking, state.CurrentMode switch
                        {
                            PlayerMovementMode.Moving => true,
                            PlayerMovementMode.Standing => false,
                            _ => throw new ArgumentOutOfRangeException()
                        });
                    });
            }
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
