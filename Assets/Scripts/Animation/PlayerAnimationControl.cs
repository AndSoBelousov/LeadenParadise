using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LeadenParadise
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationControl : MonoBehaviour
    {

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void PlayerMovementAnimation(Vector3 direction)
        {
            _animator.SetFloat("Sideways", direction.x);
            _animator.SetFloat("MoveForward", direction.z);
        }
    }
}

