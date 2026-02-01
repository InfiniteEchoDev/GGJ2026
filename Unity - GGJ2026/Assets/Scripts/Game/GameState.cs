using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace com.ggj2026teamname.gamename
{

[System.Serializable]
[CreateAssetMenu( fileName = "GameState", menuName = "Create SOs/Create GameState" )]
public class GameState : ScriptableObject
{
    public bool DidPickupBlockingSeedChapter1 = false;
    public int CountSeedsPickedUpChapter1 = 0;
    
    public int SacredChapter2 = 0;
    public int ProfaneChapter2 = 0;

    public int SacredChapter3 = 0;
    public int ProfaneChapter3 = 0;

    public bool DidPoisonChapter4 = false;
}

}
