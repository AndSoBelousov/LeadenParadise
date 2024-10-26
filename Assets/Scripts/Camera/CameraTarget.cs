using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _standartPositionCameraTarget;

    [Tooltip("����������� ����������� �������� ������")]
    [SerializeField, Range(1, 10)] private float _smoothing = 1.5f;
    [SerializeField, Range(1, 10)]
    private float _additionalOffset = 5;

    
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _standartPositionCameraTarget.position, _smoothing * Time.deltaTime);
        //transform.position = _isLooking ? Vector3.Lerp(transform.position, _standartPositionCameraTarget.position + _deltaOffset, _smoothing * Time.deltaTime) 
        //    : Vector3.Lerp(transform.position, _standartPositionCameraTarget.position, _smoothing * Time.deltaTime);
    }
}
