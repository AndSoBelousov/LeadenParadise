using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace LeadenParadise.Input
{
    public class PlayerAiming : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float coneAngle = 30f; // Угол конуса
        [SerializeField] private float coneDistance = 5f; // Дистанция конуса

        [SerializeField] private float _rotationSpeed = 5f; // Скорость вращения
        [SerializeField] private Transform _target;

        private Ray ray;
        private InputHandler _inputHandler;
        private PlayerAnimationControl _animation;
        private Vector2 lookInput;

        private WeaponController _weaponController;

        private void Awake()
        {
            _inputHandler = GetComponent<InputHandler>();
            mainCamera = Camera.allCameras[0]; 
            _animation = GetComponent<PlayerAnimationControl>();
            _weaponController = FindFirstObjectByType<WeaponController>();
        }
    

        private void Update()
        {
            lookInput = _inputHandler.LookInput;
            if (lookInput.sqrMagnitude > 0.1f)
            {
                RotateTowardsMouse(_inputHandler.LookInput);
            }
        }

        private void RotateTowardsMouse(Vector2 input)
        {
            Ray ray = mainCamera.ScreenPointToRay(input);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPoint = hit.point;
                _target.position = hitPoint;
                Vector3 direction = (hitPoint - transform.position).normalized;
                if (!IsPointWithinCone(hitPoint, transform.position, transform.forward, coneAngle))
                {
                    direction.y = 0; // Убираем влияние на вращение по оси Y
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                }
            }

        }


        private bool IsPointWithinCone(Vector3 point, Vector3 coneOrigin, Vector3 coneDirection, float coneAngle)
        {

            // Проверяем угол
            float angle = Vector3.Angle(coneDirection, (point - coneOrigin).normalized);
            return angle < coneAngle;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * coneDistance);

            Vector3 rightBoundary = Quaternion.Euler(0, coneAngle, 0) * transform.forward * coneDistance;
            Vector3 leftBoundary = Quaternion.Euler(0, -coneAngle, 0) * transform.forward * coneDistance;

            Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
            Gizmos.DrawLine(transform.position, transform.position + leftBoundary);

            // Отрисовка конуса
            Gizmos.DrawWireSphere(transform.position + transform.forward * coneDistance, 0.1f);
        }
    }
}

