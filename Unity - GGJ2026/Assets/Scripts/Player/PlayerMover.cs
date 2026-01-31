using UnityEngine;

namespace com.ggj2026teamname.gamename
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private PlayerInput_Base _playerInput;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _movementSpeed = 5f;

        void Update()
        {
            var movement = _playerInput.GetMovementVector();
            MovePlayer(movement);
        }

        private void MovePlayer(Vector2 movement)
        {
            var scaledMovement = movement * _movementSpeed;
            _rigidbody2D.linearVelocity = new Vector3(scaledMovement.x, scaledMovement.y, 0f);
        }
    }
}