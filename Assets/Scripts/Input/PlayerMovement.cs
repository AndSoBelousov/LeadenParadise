using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeadenParadise.Input
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerAnimationControl))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float forwardSpeedBooster = 1.5f;
        [SerializeField] private float sprintMultiplier = 1.5f;

        private InputHandler _inputHandler;
        private Rigidbody _rb;
        private PlayerAnimationControl _animation;

        private Vector3 globalVelocity = Vector3.zero;
        private Vector3 localVelocity = Vector3.zero;

        public Vector3 LocalMovementDirection
        {
            get
            {
                globalVelocity = _rb.velocity;
                localVelocity = transform.InverseTransformDirection(globalVelocity);
                return localVelocity.normalized;
            }
        }
       


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _inputHandler = GetComponent<InputHandler>();
        }

        private void Start()
        {
            _animation = GetComponent<PlayerAnimationControl>();
        }

        private void FixedUpdate()
        {
            Movement(_inputHandler.MoveInput);
            _animation.PlayerMovementAnimation(LocalMovementDirection);
            //Debug.Log(LocalMovementDirection);

        }

        private void Movement(Vector2 moveDirection)
        {
            Vector3 direction = new Vector3(moveDirection.x, 0, moveDirection.y);
            float speed = _inputHandler.IsSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

            if (LocalMovementDirection == Vector3.forward) direction *= forwardSpeedBooster;

            _rb.velocity = direction * speed;

        }

    }
}


