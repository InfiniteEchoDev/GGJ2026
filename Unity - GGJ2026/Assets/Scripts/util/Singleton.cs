using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace com.ggj2026teamname.gamename {

public class Singleton<SingletonClass> : MonoBehaviour where SingletonClass : MonoBehaviour
{
    public static SingletonClass Instance;

    protected virtual void Awake() {
        if( Instance != null ) {
            Destroy( gameObject );
            return;
        }

        Instance = this as SingletonClass;
    }

    void OnApplicationQuit() {
        Instance = null;
        Destroy( gameObject );
    }
}

}
