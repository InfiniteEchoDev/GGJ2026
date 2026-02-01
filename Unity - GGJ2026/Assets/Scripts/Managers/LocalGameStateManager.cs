using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace com.ggj2026teamname.gamename
{

public class LocalGameStateManager : Singleton<LocalGameStateManager>
{
    public void SetDidPickupBlockingSeedChapter1() => GameStateManager.Instance.SetDidPickupBlockingSeedChapter1();
    public void IncrementCountSeedsPickedUpChapter1() => GameStateManager.Instance.IncrementCountSeedsPickedUpChapter1();

    public void IncrementSacredChapter2() => GameStateManager.Instance.IncrementSacredChapter2();
    public void AddSacredChapter2( int sacredToAdd ) => GameStateManager.Instance.AddSacredChapter2( sacredToAdd );
    public void IncrementProfaneChapter2() => GameStateManager.Instance.IncrementProfaneChapter2();
    public void AddProfaneChapter2( int profaneToAdd ) => GameStateManager.Instance.AddProfaneChapter2( profaneToAdd );

    public void IncrementSacredChapter3() => GameStateManager.Instance.IncrementSacredChapter3();
    public void AddSacredChapter3( int sacredToAdd ) => GameStateManager.Instance.AddSacredChapter3( sacredToAdd );
    public void IncrementProfaneChapter3() => GameStateManager.Instance.IncrementProfaneChapter3();
    public void AddProfaneChapter3( int profaneToAdd ) => GameStateManager.Instance.AddProfaneChapter3( profaneToAdd );

    public void SetDidPoisonChapter4( bool didPoison ) => GameStateManager.Instance.SetDidPoisonChapter4( didPoison );
}

}
