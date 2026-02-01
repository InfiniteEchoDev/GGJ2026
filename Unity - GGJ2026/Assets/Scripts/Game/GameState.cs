using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace com.ggj2026teamname.gamename
{

public class GameState : MonoBehaviour
{
    public bool? Chapter1DidPickupBlockingSeed = null;
    public int Chapter1CountSeedsPickedUp = 0;
    
    public int Chapter2Sacred = 0;
    public int Chapter2Profane = 0;

    public int Chapter3Sacred = 0;
    public int Chapter3Profane = 0;

    public bool? Chapter4DidPoison = null;
}

}
