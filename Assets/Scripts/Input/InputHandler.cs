using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Common.Input
{
    [CreateAssetMenu(fileName = "InputHandler", menuName = "ScriptableObjects/Input Handler")]
    public class InputHandler : ScriptableObject, GameInput.IWorldInteractionActions
    {
        public UnityAction<Vector2> HorizontalMoveEvent = delegate { };
        public UnityAction<Vector2> MouseMoveEvent = delegate { };
        public UnityAction<float> VerticalMoveEvent = delegate { };
        public UnityAction<bool> SprintEvent = delegate { };

        private GameInput _gameInput;

        private void OnEnable()
        {
            if (_gameInput != null) return;
            _gameInput = new GameInput();
            _gameInput.WorldInteraction.SetCallbacks(this);

            // Enable desire input scheme
            _gameInput.WorldInteraction.Enable();
        }

        private void OnDisable()
        {
            // Disable all input schemes
            _gameInput.WorldInteraction.Disable();
        }

        public void OnHorizontalMove(InputAction.CallbackContext context)
        {
            HorizontalMoveEvent.Invoke(context.ReadValue<Vector2>());
        }

        public void OnVerticalMove(InputAction.CallbackContext context)
        {
            VerticalMoveEvent.Invoke(context.ReadValue<float>());
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SprintEvent.Invoke(true);
            }
            else if (context.canceled)
            {
                SprintEvent.Invoke(false);
            }
        }

        public void OnMouseMove(InputAction.CallbackContext context)
        {
            MouseMoveEvent.Invoke(context.ReadValue<Vector2>());
        }
    }
}
