using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace com.ggj2026teamname.gamename
{

public class BootstrapBegin : MonoBehaviour
{
    void Start() {
        GameManager.Instance.InitiateBootstrap();
    }
}

}
