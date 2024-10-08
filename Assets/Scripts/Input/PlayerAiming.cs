using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LeadenParadise.Input
{
    public class PlayerAiming : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        private InputHandler _inputHandler;

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            mainCamera = Camera.main;
        }

        private void Update()
        {
            Vector2 lookInput = _inputHandler.LookInput;
            if (lookInput.sqrMagnitude > 0.1f)
            {
                RotateTowardsMouse();
            }
        }

        private void RotateTowardsMouse()
        {
            Ray ray = mainCamera.ScreenPointToRay(_inputHandler.LookInput);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 direction = (hitPoint - transform.position).normalized;
                direction.y = 0; // Убираем влияние на вращение по оси Y
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
            }
        }
    }
}

