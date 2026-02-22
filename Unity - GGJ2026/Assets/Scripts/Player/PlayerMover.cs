using System;
using R3;
using UnityEngine;

namespace com.ggj2026teamname.gamename
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private PlayerInput_Base _playerInput;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _movementSpeed = 5f;
        
        private readonly ReactiveProperty<PlayerMovementState> _movementState = new(PlayerMovementState.Default);
        public ReadOnlyReactiveProperty<PlayerMovementState> MovementState => _movementState;
        
        void Update()
        {
            var movement = _playerInput.GetMovementVector();
            MovePlayer(movement);
        }

        private void MovePlayer(Vector2 movement)
        {
            var scaledMovement = movement * _movementSpeed;
            _rigidbody2D.linearVelocity = new Vector3(scaledMovement.x, scaledMovement.y, 0f);

            var isFacingRight = _movementState.Value.IsFacingRight;
            var mode = PlayerMovementMode.Standing;

            if (!Mathf.Approximately(movement.x, 0))
            {
                isFacingRight = movement.x > 0;
            }
            
            if (!Mathf.Approximately(movement.sqrMagnitude, 0))
            {
                mode = PlayerMovementMode.Moving;
            }
            
            var state = new PlayerMovementState(mode, isFacingRight);

            _movementState.Value = state;
        }
    }

    public readonly struct PlayerMovementState : IEquatable<PlayerMovementState>
    {
        public readonly bool IsFacingRight;
        public readonly PlayerMovementMode CurrentMode;

        public PlayerMovementState(PlayerMovementMode currentMode, bool isFacingRight)
        {
            CurrentMode = currentMode;
            IsFacingRight = isFacingRight;
        }

        public static PlayerMovementState Default => new(PlayerMovementMode.Standing, true);

        public bool Equals(PlayerMovementState other)
        {
            return IsFacingRight == other.IsFacingRight && CurrentMode == other.CurrentMode;
        }

        public override bool Equals(object obj)
        {
            return obj is PlayerMovementState other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsFacingRight, (int)CurrentMode);
        }
    }

    public enum PlayerMovementMode
    {
        Standing,
        Moving
    }
}