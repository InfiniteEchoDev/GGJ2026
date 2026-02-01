using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;


namespace com.ggj2026teamname.gamename
{

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    LocalSceneManager _activeLocalSceneManager;
    public LocalSceneManager ActiveLocalSceneManager { get => _activeLocalSceneManager; }

    protected override void Awake() {
        DontDestroyOnLoad( gameObject );

        base.Awake();
    }

    void Start() {
    }

    public void InitiateBootstrap()
    {
        StartCoroutine( SwitchToNewSceneAsync( GameScene.MainMenu ) );
    }

    public void SwitchToNewScene( GameScene toSwitchTo ) => StartCoroutine( SwitchToNewSceneAsync( toSwitchTo ) );
    IEnumerator SwitchToNewSceneAsync( GameScene toSwitchTo ) {
        if( _activeLocalSceneManager != null && !_activeLocalSceneManager.OnBeforeLoadOtherSceneAsync() )
            yield break;
        if( _activeLocalSceneManager != null )
            _activeLocalSceneManager.OnEndingScene();

        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync( ScenesManager.Instance.GetSceneBuildIdx( toSwitchTo ), LoadSceneMode.Single );
        yield return loadSceneAsync;

        SetActiveLocalSceneManagerAndBegin( LocalSceneManager.Instance );
    }

    void SetActiveLocalSceneManagerAndBegin( LocalSceneManager localSceneManager )
    {
        _activeLocalSceneManager = localSceneManager;
        _activeLocalSceneManager.OnBeginScene();
    }

    IEnumerator LoadSceneAfterSeconds( float seconds ) {
        yield return new WaitForSeconds( seconds );

        StartCoroutine( SwitchToNewSceneAsync( GameScene.Game ) );
    }

    public void OnBootstrapInPlayMode( LocalSceneManager playModeLocalSceneManager ) {
        SetActiveLocalSceneManagerAndBegin( playModeLocalSceneManager );
    }

}
}
