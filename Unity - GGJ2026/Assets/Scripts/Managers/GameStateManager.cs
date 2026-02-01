using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace com.ggj2026teamname.gamename
{

public class GameStateManager : Singleton<GameStateManager>
{

    public GameState CurrentGameState { private set; get; }

    
    protected override void Awake() {
        base.Awake();
    }

    public void SetChapter1DidPickupBlockingSeed( bool didPickupBlockingSeed ) {
        CurrentGameState.Chapter1DidPickupBlockingSeed = didPickupBlockingSeed;
    }
    public void IncrementChapter1CountSeedsPickedUp() => CurrentGameState.Chapter1CountSeedsPickedUp++;

    public int IncrementChapter2Sacred() => AddChapter2Sacred( 1 );
    public int AddChapter2Sacred( int sacredToAdd ) {
        CurrentGameState.Chapter2Sacred += sacredToAdd;

        return sacredToAdd;
    }
    public int IncrementChapter2Profane() => AddChapter2Profane( 1 );
    public int AddChapter2Profane( int profaneToAdd ) {
        CurrentGameState.Chapter2Sacred += profaneToAdd;

        return profaneToAdd;
    }

    public int IncrementChapter3Sacred() => AddChapter3Sacred( 1 );
    public int AddChapter3Sacred( int sacredToAdd ) {
        CurrentGameState.Chapter2Sacred += sacredToAdd;

        return sacredToAdd;
    }
    public int IncrementChapter3Profane() => AddChapter3Profane( 1 );
    public int AddChapter3Profane( int profaneToAdd ) {
        CurrentGameState.Chapter2Sacred += profaneToAdd;

        return profaneToAdd;
    }

    public void SetChapter4DidPoison( bool didPoison ) {
        CurrentGameState.Chapter4DidPoison = didPoison;
    }
}

}
