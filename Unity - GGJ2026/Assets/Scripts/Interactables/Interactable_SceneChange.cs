using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.ggj2026teamname.gamename.Interactables
{
    public class Interactable_SceneChange : Interactable_Base
    {
        [SerializeField] private GameScene _sceneToSwitchTo;
        public override void Interact()
        {
            GameManager.Instance.SwitchToScene(_sceneToSwitchTo);
        }
    }
}