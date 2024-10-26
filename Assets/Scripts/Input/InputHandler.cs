using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace LeadenParadise.Input
{
    public class InputHandler : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool IsSprinting { get; private set; }
        public bool TakeAimButton { get; private set; }

        public delegate void ShotAction();
        public static event ShotAction OnShotPressed;

        private bool _shotButtonPressed;

        private CustomController _playerInput;

        private void Awake()
        {
            _playerInput = new CustomController();

            _playerInput.TopViewControl.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            _playerInput.TopViewControl.Move.canceled += ctx => MoveInput = Vector2.zero;

            _playerInput.TopViewControl.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
            _playerInput.TopViewControl.Look.canceled += ctx => LookInput = Vector2.zero;

            _playerInput.TopViewControl.Sprint.performed += ctx => IsSprinting = true;
            _playerInput.TopViewControl.Sprint.canceled += ctx => IsSprinting = false;

            _playerInput.TopViewControl.TakeAim.performed += ctx => TakeAimButton = true;
            _playerInput.TopViewControl.TakeAim.canceled += ctx => TakeAimButton = false;

            _playerInput.TopViewControl.Shot.performed += OnShot;
            _playerInput.TopViewControl.Shot.canceled += OnShotCanceled;


        }

        private void OnShot(InputAction.CallbackContext context)
        {
            if (!_shotButtonPressed)
            {

                _shotButtonPressed = true;
                OnShotPressed?.Invoke();
            }
        }
        private void OnShotCanceled(InputAction.CallbackContext context)
        {
            _shotButtonPressed = false;
        }

        private void OnEnable() => _playerInput.Enable();

        private void OnDisable() => _playerInput.Disable();
    }
}

