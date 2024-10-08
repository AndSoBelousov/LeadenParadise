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
        }

        private void OnEnable() => _playerInput.Enable();

        private void OnDisable() => _playerInput.Disable();
    }
}

