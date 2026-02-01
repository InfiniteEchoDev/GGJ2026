using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace com.ggj2026teamname.gamename
{

public class PlayerManager : Singleton<PlayerManager>
{
    [Header( "Obj Refs" )]
    public Player Player;
    public Camera PlayerCamera;
    
    protected override void Awake() {
        base.Awake();

        if( PlayerCamera == null )
            PlayerCamera = Player.GetComponentInChildren<Camera>();
    }


    void DoPickupBlockingSeedChapter1() {
        // TODO: Player anim

        GameStateManager.Instance.SetDidPickupBlockingSeedChapter1();
    }
    void DoPickupSeedChapter1() {
        // TODO: Player anim

        GameStateManager.Instance.IncrementCountSeedsPickedUpChapter1();
    }

    void DoAddSacredChapter2( int sacredToAdd ) {
        // TODO: Player anim

        GameStateManager.Instance.AddSacredChapter2( sacredToAdd );
    }
    void DoAddProfaneChapter2( int profaneToAdd ) {
        // TODO: Player anim

        GameStateManager.Instance.AddProfaneChapter2( profaneToAdd );
    }

    void DoAddSacredChapter3( int sacredToAdd ) {
        // TODO: Player anim

        GameStateManager.Instance.AddSacredChapter3( sacredToAdd );
    }
    void DoAddProfaneChapter3( int profaneToAdd ) {
        // TODO: Player anim

        GameStateManager.Instance.AddProfaneChapter3( profaneToAdd );
    }
    
    void DoPoisonChoiceChapter4( bool didPoison ) {
        // TODO: Player anim

        GameStateManager.Instance.SetDidPoisonChapter4( didPoison );
    }
}

}
