using System.Collections;
using System.Collections.Generic;

using Pixelplacement;

using UnityEngine;
using UnityEngine.UI;


namespace com.ggj2026teamname.gamename
{

public class LocalSceneManager : Singleton<LocalSceneManager>
{

    public GameObject GameManagerPrefab;
    public MaskOverlayController MaskOverlayController;
    public Image SceneFader;

    protected override void Awake() {
        base.Awake();

        if( GameManager.Instance == null ) {
            GameManager gameManager = Instantiate( GameManagerPrefab ).GetComponent<GameManager>();
            gameManager.OnBootstrapInPlayMode( this );
        }
    }


    public void OnBeginScene() {
        // Prepare anything the scene might need
        // Runs before Unity->Awake()
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
