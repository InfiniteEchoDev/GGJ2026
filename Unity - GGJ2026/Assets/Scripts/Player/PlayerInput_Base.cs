using System;
using UnityEngine;

namespace com.ggj2026teamname.gamename
{
    public abstract class PlayerInput_Base : MonoBehaviour
    {
        
        public abstract Vector2 GetMovementVector();
        public abstract void RegisterInteractAction(Action action);
        public abstract void DeregisterInteractAction(Action action);


    }
}