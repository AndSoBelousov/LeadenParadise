using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletShot : MonoBehaviour
{
    [SerializeField]
    private float _pushforce = 3;
    private Rigidbody _rb;


    private void Awake()
    {

        
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        ShellCasingPush();
    }

    private void ShellCasingPush()
    {
        

        _rb.AddForce(Vector3.forward * _pushforce, ForceMode.Impulse);
    }

}
