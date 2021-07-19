using System;
using Assets.Scripts.Common.Input;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler = default;
        
        [Header("Movement options")]
        [Range(1, 25)]
        [SerializeField] private float _speed = 1f;
        [Range(1, 10)]
        [SerializeField] private float _sprintMultiplier = 2f;
        [Range(1, 20)]
        [SerializeField] private float _lookSensitivity = 5f;

        private Rigidbody _rigidbody = default;

        private Vector3 _moveDirection = Vector3.zero;
        private Vector2 _mouseDelta = Vector2.zero;

        private bool _sprinting = false;

        private void OnEnable()
        {
            _inputHandler.HorizontalMoveEvent += OnMoveHorizontal;
            _inputHandler.VerticalMoveEvent += OnMoveVertical;
            _inputHandler.MouseMoveEvent += OnMoveMouse;
            _inputHandler.SprintEvent += OnSprint;

            _rigidbody = GetComponent<Rigidbody>();

            Cursor.visible = false;
        }

        private void OnDisable()
        {
            _inputHandler.HorizontalMoveEvent -= OnMoveHorizontal;
            _inputHandler.VerticalMoveEvent -= OnMoveVertical;
            _inputHandler.MouseMoveEvent -= OnMoveMouse;
            _inputHandler.SprintEvent -= OnSprint;

            Cursor.visible = true;
        }

        private void OnMoveHorizontal(Vector2 direction)
        {
            _moveDirection.x = direction.x;
            _moveDirection.z = direction.y;
        }

        private void OnMoveVertical(float direction)
        {
            _moveDirection.y = direction;
        }
        
        private void OnMoveMouse(Vector2 direction)
        {
            _mouseDelta = direction;
        }
        
        private void OnSprint(bool pressed)
        {
            _sprinting = pressed;
        }

        private void FixedUpdate()
        {
            Look();
            Move();
        }

        private void Move()
        {
            var velocity = _moveDirection * (_speed * 10 * Time.fixedDeltaTime);
            if (_sprinting) velocity *= _sprintMultiplier;

            var euler = transform.rotation.eulerAngles;
            velocity = Quaternion.Euler(euler) * velocity;

            var targetPosition = _rigidbody.position + velocity;

            _rigidbody.MovePosition(targetPosition);
        }

        private void Look()
        {
            var euler = transform.rotation.eulerAngles;
            euler.y += _mouseDelta.x * (_lookSensitivity * 10 * Time.fixedDeltaTime);
            euler.x -= _mouseDelta.y * (_lookSensitivity * 10 * Time.fixedDeltaTime);
            
            _rigidbody.MoveRotation(Quaternion.Euler(euler.x, euler.y, 0.0f));
        }
    }
}
