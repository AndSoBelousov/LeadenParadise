using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace LeadenParadise
{
    public class CameraFollow : MonoBehaviour
    {

        [SerializeField] private Transform _cameraTarget;

        [SerializeField] private Transform _temporaryCameraPosition;

        [Tooltip("Коэффициент сглаживания движения камеры")]
        [SerializeField, Range(1, 10)] private float _smoothing = 2.5f;

        [Tooltip("Скорость скролла камеры при наведении мышки на край экрана")]
        [SerializeField] private float mouseScrollSpeed = 20f;

        [Tooltip("Зона, в которой будет срабатывать скролл мышки")]
        [SerializeField] private float mouseScrollZone = 30f;

        [Tooltip("Отдаленность камеры по оси Z")]
        [SerializeField] private float zDistance = 5f;

        [Tooltip("Отдаленность камеры по оси Y")]
        [SerializeField] private float yDistance = 5f;

        [Tooltip("Максимальный отступ цели от центра экрана")]
        [SerializeField] private float _deltaCamera = 0f;

        private float screenWidth;
        private float screenHeight;
        private InputHandler _input;
        private Vector3 _targetOffset;
        //private Vector3 _temporaryCameraPosition;   
        //private bool _temporaryPos = false;
        //private bool _boolShifter

        private void Awake()
        {
            _cameraTarget = FindObjectOfType<CameraTarget>().transform;
            _input = FindObjectOfType<InputHandler>();

            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }

        private void Update()
        {
            //HandleMouseScroll(_input.LookInput);
            if (_input != null && _input.TakeAimButton)
            {
                transform.position = Vector3.Lerp(transform.position, _temporaryCameraPosition.position, _smoothing * Time.deltaTime);

                transform.LookAt(_cameraTarget);
            }
            else
            {

                   FollowTarget();
            }
        }

        private void HandleMouseScroll(Vector3 pos)
        {
            //Vector3 newPosition = transform.position;
            //////if (pos.x > screenWidth - mouseScrollZone || pos.x < mouseScrollZone || pos.y > screenHeight - mouseScrollZone || pos.y < mouseScrollZone)
            //////{
            //////    _cameraTarget.LookOverEdge(true);
            //////}

            //if (pos.x > screenWidth - mouseScrollZone )
            //{
            //    newPosition.x -= mouseScrollSpeed * Time.deltaTime;
            //    _cameraTarget.LookOverEdge(true);
            //    //_temporaryCameraPosition = newPosition;
            //    //_temporaryPos = true;
            //}
            //else if (pos.x < mouseScrollZone)
            //{
            //    newPosition.x += mouseScrollSpeed * Time.deltaTime;
            //    _cameraTarget.LookOverEdge(true);
            //    //_temporaryCameraPosition = newPosition;
            //    //_temporaryPos = true;
            //}

            //if (pos.y > screenHeight - mouseScrollZone)
            //{
            //    newPosition.z -= mouseScrollSpeed * Time.deltaTime;
            //    _cameraTarget.LookOverEdge(true);
            //    //_temporaryCameraPosition = newPosition;
            //    //_temporaryPos = true;
            //}
            //else if (pos.y < mouseScrollZone)
            //{
            //    newPosition.z += mouseScrollSpeed * Time.deltaTime;
            //    _cameraTarget.LookOverEdge(true);
            //    //_temporaryCameraPosition = newPosition;
            //    //_temporaryPos = true;
            //}

            //transform.position = newPosition;
        }

        private void FollowTarget()
        {
            Vector3 targetPosition = _cameraTarget.position + new Vector3(0, yDistance, -zDistance);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothing * Time.deltaTime);
            transform.position = smoothedPosition;

            transform.LookAt(_cameraTarget);
        }
    }
}


