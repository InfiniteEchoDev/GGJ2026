using UnityEngine;
using UnityEngine.InputSystem;

namespace com.ggj2026teamname.gamename
{
    public class PlayerInput_NewInputSystem : PlayerInput_Base, InputSystem_Actions.IPlayerActions
    {
        private Vector2 _movementVector;
        
         private InputSystem_Actions m_Actions;                  // Source code representation of asset.
         private InputSystem_Actions.PlayerActions m_Player;     // Source code representation of action map.
    
         void Awake()
         {
             m_Actions = new InputSystem_Actions();              // Create asset object.
             m_Player = m_Actions.Player;                      // Extract action map object.
             m_Player.AddCallbacks(this);                      // Register callback interface IPlayerActions.
         }
        
        void OnDestroy()
         {
             m_Actions.Dispose();                              // Destroy asset object.
         }
    
         void OnEnable()
         {
             m_Player.Enable();                                // Enable all actions within map.
         }
    
         void OnDisable()
         {
             m_Player.Disable();                               // Disable all actions within map.
         }
         
        public override Vector2 GetMovementVector()
        {
            return _movementVector;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
           _movementVector = context.ReadValue<Vector2>();
           Debug.Log("Movement Vector: " + _movementVector);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            //..
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            //..
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            //..
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            //..
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            //..
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            //..
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            //..
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            //..  s
        }
    }
}