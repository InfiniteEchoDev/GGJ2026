using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;


namespace com.ggj2026teamname.gamename
{

public class GameStateManager : Singleton<GameStateManager>
{

    public GameState CurrentGameState { private set; get; }

    public Canvas DebugCanvas;
    public TMP_Text DebugText;


    public event System.Action OnUpdatedGameState;

    
    protected override void Awake() {
        base.Awake();

        if( CurrentGameState == null )
            CurrentGameState = new GameState();

        OnUpdatedGameState += UpdateDebugGameState;
        OnUpdatedGameState?.Invoke();
    }


    public void SetupLocalSceneConnections() {
    }
    
    void UpdateDebugGameState() {
        DebugText.text =
            $"Ch1:\n"
            + $"  Did pickup blocking seed: {CurrentGameState.DidPickupBlockingSeedChapter1}\n"
            + $"  Count seeds picked up: {CurrentGameState.CountSeedsPickedUpChapter1}\n"
            + $"Ch2:\n"
            + $"  Sacred count: {CurrentGameState.SacredChapter2}\n"
            + $"  Profane count: {CurrentGameState.ProfaneChapter2}\n"
            + $"Ch3:\n"
            + $"  Sacred count: {CurrentGameState.SacredChapter3}\n"
            + $"  Profane count: {CurrentGameState.ProfaneChapter3}\n"
            + $"Ch4:\n"
            + $"  Did poison: {CurrentGameState.DidPoisonChapter4}\n"
        ;
    }


    public void SetDidPickupBlockingSeedChapter1() {
        CurrentGameState.DidPickupBlockingSeedChapter1 = true;

        OnUpdatedGameState?.Invoke();
    }
    public void IncrementCountSeedsPickedUpChapter1() {
        CurrentGameState.CountSeedsPickedUpChapter1++;

        OnUpdatedGameState?.Invoke();
    }

    public int IncrementSacredChapter2() => AddSacredChapter2( 1 );
    public int AddSacredChapter2( int sacredToAdd ) {
        CurrentGameState.SacredChapter2 += sacredToAdd;

        OnUpdatedGameState?.Invoke();

        return sacredToAdd;
    }
    public int IncrementProfaneChapter2() => AddProfaneChapter2( 1 );
    public int AddProfaneChapter2( int profaneToAdd ) {
        CurrentGameState.SacredChapter2 += profaneToAdd;

        OnUpdatedGameState?.Invoke();

        return profaneToAdd;
    }

    public int IncrementSacredChapter3() => AddSacredChapter3( 1 );
    public int AddSacredChapter3( int sacredToAdd ) {
        CurrentGameState.SacredChapter2 += sacredToAdd;

        OnUpdatedGameState?.Invoke();

        return sacredToAdd;
    }
    public int IncrementProfaneChapter3() => AddProfaneChapter3( 1 );
    public int AddProfaneChapter3( int profaneToAdd ) {
        CurrentGameState.SacredChapter2 += profaneToAdd;

        OnUpdatedGameState?.Invoke();

        return profaneToAdd;
    }

    public void SetDidPoisonChapter4( bool didPoison ) {
        CurrentGameState.DidPoisonChapter4 = didPoison;

        OnUpdatedGameState?.Invoke();
    }
}

}
