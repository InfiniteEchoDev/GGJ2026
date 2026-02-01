using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace com.ggj2026teamname.gamename
{

public class LocalSceneManager : Singleton<LocalSceneManager>
{

    public GameObject GameManagerPrefab;
    public MaskOverlayController MaskOverlayController;

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

    }
    public void DoQuitGame() {
        Application.Quit();
    }
}

}
