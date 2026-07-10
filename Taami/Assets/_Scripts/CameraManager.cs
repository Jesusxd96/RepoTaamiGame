using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform target;
    private float _distanceToPlayer; //Distancia de la camara al jugador. Eventualmente se podra modificar

    private Vector2 _input;
    [SerializeField] private MouseSensitivity mouseSensitivity;
    [SerializeField] private CameraAngle cameraAngle;

    private CameraRotation _cameraRotation;
    #endregion
    private void Awake()
    {
        _distanceToPlayer = Vector3.Distance(transform.position,target.position);//Distancia entre la camara y el jugador, puede servir para hacer Zoom
    }

    public void Look(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();//Nos dara la posicion del mouse
    }
    public void Scroll()//Para poder acercar y alejar la camara
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            _distanceToPlayer--;
        }
        else
        {
            if (Input.mouseScrollDelta.y < 0)
            {
                _distanceToPlayer++;
            }
        }
    }
    private void Update()
    {
        Scroll();
        _cameraRotation.Yaw += _input.x * mouseSensitivity.horizontal*BoolToInt(mouseSensitivity.invertHorizontal) * Time.deltaTime;
        _cameraRotation.Pitch += _input.y * mouseSensitivity.vertical * BoolToInt(mouseSensitivity.invertVertical) * Time.deltaTime;
        _cameraRotation.Pitch = Mathf.Clamp(_cameraRotation.Pitch, cameraAngle.min, cameraAngle.max);
    }
    private void LateUpdate() {
        transform.eulerAngles = new Vector3(_cameraRotation.Pitch, _cameraRotation.Yaw, 0.0f);//Rotate the canmera
        transform.position = target.position - transform.forward * _distanceToPlayer;
    }
    private static int BoolToInt(bool b) => b ? 1 : -1;
}

[Serializable]/*Este Struct se hizo para mantener mas limpio el codigo*/
public struct MouseSensitivity
{
    public float horizontal;
    public float vertical;
    public bool invertVertical;//Para mira invertida
    public bool invertHorizontal;
}
public struct CameraRotation
{
    public float Pitch;
    public float Yaw;
}
[Serializable]
public struct CameraAngle
{
    public float min;
    public float max;
}