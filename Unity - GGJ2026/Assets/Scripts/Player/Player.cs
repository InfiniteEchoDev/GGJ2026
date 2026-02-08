using System;
using R3;
using UnityEngine;

namespace com.ggj2026teamname.gamename
{
    public class Player : MonoBehaviour
    {
        private LocalSceneManager _localSceneManager;
        
        [SerializeField] private PlayerInput_NewInputSystem inputSystem;
        
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
