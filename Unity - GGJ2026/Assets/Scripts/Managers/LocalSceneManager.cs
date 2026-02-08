using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Pixelplacement;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;


namespace com.ggj2026teamname.gamename
{

public class LocalSceneManager : Singleton<LocalSceneManager>
{
    public Player Player { get; private set; }
    public Camera PlayerCamera;
    public GameObject GameManagerPrefab;
    public DialogueRunner DialogueRunner;
    public MaskOverlayController MaskOverlayController;
    public Image SceneFader;
    
    public Observable<bool> IsDialogueRunning { get; private set; }
    public Observable<bool> IsGlobalAnimationRunning { get; private set; }

    protected override void Awake() {
        base.Awake();

        if( GameManager.Instance == null ) {
            GameManager gameManager = Instantiate( GameManagerPrefab ).GetComponent<GameManager>();
            gameManager.OnBootstrapInPlayMode( this );
        }
    }

    public async UniTask OnBeginScene() {
        // Prepare anything the scene might need
        // Runs before Unity->Awake()

        if (DialogueRunner)
        {
            IsDialogueRunning = Observable
                .EveryValueChanged(DialogueRunner, runner => runner.IsDialogueRunning, destroyCancellationToken);
        }
        else
        {
            IsDialogueRunning = Observable.Return(false);
        }

        if (MaskOverlayController)
        {
            IsGlobalAnimationRunning = MaskOverlayController.IsShowingMask;
        }
        else
        {
            IsGlobalAnimationRunning = Observable.Return(false);
        }

        Player = FindFirstObjectByType<Player>();

        // wait for whatever shit needs to do its Awake() and Update() shenanigans before beginning the game flow
        await UniTask.DelayFrame(1);

        if (MaskOverlayController)
        {
            MaskOverlayController.Begin();
        }
        
        if (Player)
        {
            Player.Begin();
        }
    }

    public bool OnBeforeLoadOtherSceneAsync()
    {
        // Potentially prevent loading another scene by returning false
        return true;
    }

    public void OnEndingScene()
    {
        // Clean anything up we need to clean up
    }


    public void InitiateLoadScene( GameScene sceneToLoad ) {
        GameManager.Instance.SwitchToScene( sceneToLoad );
    }

    public void DoBeginGame() {
        // GameManager.Instance.SwitchToScene( GameScene.Chapter_01 );
        MaskOverlayController.MainMenuFadeOutForSceneChange();
        Tween.Color( SceneFader, Color.black, 2.5f, 0, Tween.EaseOut );
    }
    public void DoQuitGame() {
        Application.Quit();
    }
}

}
