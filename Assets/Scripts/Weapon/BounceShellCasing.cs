using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceShellCasing : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _bounceShellCasing = new Vector3(1f, 1f, 0f);
    [SerializeField]
    private float _pushforce = 3;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _rb.AddForce(_bounceShellCasing * _pushforce, ForceMode.Impulse);
    }
}
