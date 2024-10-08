using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("������ ���������")]
    [SerializeField] private Transform _player;

    [Tooltip("����������� ����������� �������� ������")]
    [SerializeField, Range(1, 10)] public float _smoothing = 2.5f;

    [Tooltip("�������� ������� ������ ��� ��������� ����� �� ���� ������ ")]
    [SerializeField] private float mouseScrollSpeed = 20f;

    [Tooltip("����, � ������� ����� ����������� ������ �����")]
    [SerializeField] private float mouseScrollZone = 30f;

    private float screenWidth; 
    private float screenHeight;

    private InputHandler _input;
    private Vector3 _offset;

    private void Awake()
    {
        _player = FindAnyObjectByType<InputHandler>().gameObject.transform;
        _input = FindAnyObjectByType<InputHandler>();
    }

    void Start()
    {
        _offset = transform.position - _player.position;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void LateUpdate()
    {
        Vector3 targetCamPos = _player.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, _smoothing * Time.deltaTime);

        LookTheEdge(_input.LookInput); 
    }

    // ��� ��������� ������� �� ���� ������, ������ ��������� � ��� �������
    private void LookTheEdge(Vector3 pos)
    {
        if (pos.x < mouseScrollZone)
        {
            transform.Translate(Vector3.left * mouseScrollSpeed * Time.deltaTime);
        }
        else if (pos.x > screenWidth - mouseScrollZone)
        {
            transform.Translate(Vector3.right * mouseScrollSpeed * Time.deltaTime);
        }

        if (pos.y < mouseScrollZone)
        {
            transform.Translate(Vector3.back * mouseScrollSpeed * Time.deltaTime);
        }
        else if (pos.y > screenHeight - mouseScrollZone)
        {
            transform.Translate(Vector3.forward * mouseScrollSpeed * Time.deltaTime);
        }
    }
}
