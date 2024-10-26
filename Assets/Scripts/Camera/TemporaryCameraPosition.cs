using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryCameraPosition : MonoBehaviour
{
    private Vector3 _position;

    [SerializeField, Range(0.1f, 1)]
    private float _delta = 0.3f;

    private bool _isPositionRight = true;

    private void Start()
    {
        _position = transform.localPosition;
    }
    public void Toggle()
    {
        transform.position = _isPositionRight ? transform.localPosition - Vector3.right * _delta : transform.localPosition + Vector3.right * _delta;
        
        _isPositionRight = !_isPositionRight;

    }

}
