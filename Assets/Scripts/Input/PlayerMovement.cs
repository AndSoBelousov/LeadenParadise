using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeadenParadise.Input
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float sprintMultiplier = 1.5f;

        private InputHandler _inputHandler;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<InputHandler>();
        }

        private void FixedUpdate()
        {
            Vector2 moveDirection = _inputHandler.MoveInput;
            Vector3 direction = new Vector3(moveDirection.x, 0, moveDirection.y);
            float speed = _inputHandler.IsSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
            _rb.velocity = direction * speed;
        }
    }
}


