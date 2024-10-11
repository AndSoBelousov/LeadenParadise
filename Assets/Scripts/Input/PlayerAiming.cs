using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LeadenParadise.Input
{
    public class PlayerAiming : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        private Ray ray;
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
                RotateTowardsMouse(_inputHandler.LookInput);
            }
        }

        private void RotateTowardsMouse(Vector2 input)
        {
            ray = mainCamera.ScreenPointToRay(input);
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

        private void AimAtMouse(Vector2 input)
        {
            

            //if (Physics.Raycast(ray, out RaycastHit hit))
            //{
            //    Vector3 targetDirection = hit.point - upperBody.position;
            //    targetDirection.y = 0; // Ограничиваем поворот по вертикали
            //    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            //    // Плавно вращаем верхнюю часть
            //    upperBody.rotation = Quaternion.Slerp(upperBody.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}

