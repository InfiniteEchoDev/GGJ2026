using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.ggj2026teamname.gamename
{
    public class PlayerInput_NewInputSystem : PlayerInput_Base, InputSystem_Actions.IPlayerActions
    {
         private Vector2 _movementVector;
         private Action _onInteract;
        
         private InputSystem_Actions _actions;                  
         private InputSystem_Actions.PlayerActions _playerActions;     
    
         private void Awake()
         {
             _actions = new InputSystem_Actions();              
             _playerActions = _actions.Player;                      
             _playerActions.AddCallbacks(this);
         }
        
         private void OnDestroy()
         {
             _actions.Dispose();
             _onInteract = null;
         }
    
         private void OnEnable()
         {
             _playerActions.Enable();
         }
    
         private void OnDisable()
         {
             _playerActions.Disable();
         }

         public void SetPlayerInputState(bool isActivated)
         {
             if(isActivated)
             {
                 _playerActions.Enable();
             }
             else
             {
                 _playerActions.Disable();
             }
         }
         
        public override Vector2 GetMovementVector()
        {
            return _movementVector;
        }

        public override void RegisterInteractAction(Action action)
        {
            _onInteract += action;
        }

        public override void DeregisterInteractAction(Action action)
        {
            _onInteract -= action;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
           _movementVector = context.ReadValue<Vector2>();
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
            if(context.started)
            {
                _onInteract?.Invoke();
            }
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