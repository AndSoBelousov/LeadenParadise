using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _standartPositionCameraTarget;

    [Tooltip("Коэффициент сглаживания движения камеры")]
    [SerializeField, Range(1, 10)] private float _smoothing = 1.5f;
    [SerializeField, Range(1, 10)]
    private float _additionalOffset = 5;

    //private Vector3 _deltaOffset;
    //[SerializeField]
    //private bool _isLooking = false;

    //private void Toggle()
    //{
    //    _isLooking = !_isLooking;
    //}

    //public void LookOverEdge(bool deltaBool)
    //{
    //    _deltaOffset = Vector3.fwd * _additionalOffset;
    //    _isLooking = deltaBool;
    //}
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _standartPositionCameraTarget.position, _smoothing * Time.deltaTime);
        //transform.position = _isLooking ? Vector3.Lerp(transform.position, _standartPositionCameraTarget.position + _deltaOffset, _smoothing * Time.deltaTime) 
        //    : Vector3.Lerp(transform.position, _standartPositionCameraTarget.position, _smoothing * Time.deltaTime);
    }
}
