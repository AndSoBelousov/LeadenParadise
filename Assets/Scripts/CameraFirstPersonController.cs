using LeadenParadise.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFirstPersonController : MonoBehaviour
{
    public float sensitivity = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;

    private InputHandler _input;

    private void Awake()
    {
        _input = FindFirstObjectByType<InputHandler>();
    }

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        _rotationX -= _input.LookInput.y * sensitivity;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

        float delta = _input.LookInput.x * sensitivity;// величина на которую следует поменять угол поворота
        float rotationY = transform.localEulerAngles.y + delta;// приращение угла поворота через значение delta

        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0); // новый вектор из сохраяненных значений поворота
    }
}
