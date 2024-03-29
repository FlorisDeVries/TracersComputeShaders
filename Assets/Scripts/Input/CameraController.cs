﻿using UnityEngine;

namespace Assets.Scripts.Input
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

        private Vector3 _moveDirection = Vector3.zero;
        private Vector2 _mouseDelta = Vector2.zero;

        private bool _sprinting = false;
        private bool _pauseInput = false;

        private void OnEnable()
        {
            _inputHandler.HorizontalMoveEvent += OnMoveHorizontal;
            _inputHandler.VerticalMoveEvent += OnMoveVertical;
            _inputHandler.MouseMoveEvent += OnMoveMouse;
            _inputHandler.SprintEvent += OnSprint;
            _inputHandler.EscapeEvent += OnEscape;

            Cursor.visible = false;
        }

        private void OnDisable()
        {
            _inputHandler.HorizontalMoveEvent -= OnMoveHorizontal;
            _inputHandler.VerticalMoveEvent -= OnMoveVertical;
            _inputHandler.MouseMoveEvent -= OnMoveMouse;
            _inputHandler.SprintEvent -= OnSprint;
            _inputHandler.EscapeEvent -= OnEscape;

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
            _mouseDelta = Vector2.ClampMagnitude(direction, 2);
        }
        
        private void OnSprint(bool pressed)
        {
            _sprinting = pressed;
        }

        private void OnEscape()
        {
            _pauseInput = !_pauseInput;
        }


        private void Update()
        {
            Look();
            Move();
        }

        private void Move()
        {
            if (_pauseInput)
                return;

            var velocity = _moveDirection * (_speed * 10 * Time.deltaTime);
            if (_sprinting) velocity *= _sprintMultiplier;

            var euler = transform.rotation.eulerAngles;
            velocity = Quaternion.Euler(euler) * velocity;

            var targetPosition = transform.position + velocity;

            transform.position = targetPosition;
        }

        private void Look()
        {
            if (_pauseInput)
                return;

            var euler = transform.rotation.eulerAngles;
            euler.y += _mouseDelta.x;
            euler.x -= _mouseDelta.y;

            var targetRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler.x, euler.y, 0.0f), _lookSensitivity * Time.deltaTime * 10);

            transform.rotation = targetRotation;
        }
    }
}
