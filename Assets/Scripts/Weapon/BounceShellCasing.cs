using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceShellCasing : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _bounceShellCasing = new Vector3(-1f, -1f, 0f);
    [SerializeField]
    private float _pushforce = 3;
    private Vector3 _transformedDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _transformedDirection = transform.TransformDirection(_bounceShellCasing);
        _rb.AddForce(_transformedDirection * _pushforce, ForceMode.Impulse);
    }
}
