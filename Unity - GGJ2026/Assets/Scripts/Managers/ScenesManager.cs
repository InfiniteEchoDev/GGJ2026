using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace com.ggj2026teamname.gamename
{

public enum GameScene {
    Bootstrap = 0,
    MainMenu,
    Game,
}

public class ScenesManager : Singleton<ScenesManager>
{
    
    // [Header( "Obj Refs" )]
    // public GameObject thing;
    
    protected override void Awake() {
        base.Awake();
    }
    
    
    public int GetSceneBuildIdx( GameScene gameSceneToGet ) => (int)gameSceneToGet;
}

}
