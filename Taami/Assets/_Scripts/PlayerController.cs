using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]//Esto se asegura de que no se pueda quitar algun componente del objeto
public class PlayerController : MonoBehaviour
{
    /*For testing purposes we are using the old input system, once it moves we will be moving from this to the new one*/
    public Transform cam;
    public Rigidbody rb;//El rigidbody del player.

    /*Variables de movimiento y del jugador*/
    private CharacterController controller;
    private Vector2 _input;//Input del jugador.
    private Vector3 _direction;
    [SerializeField]private float speed = 6f; //La velocidad a la que se mueve el personaje
    [SerializeField]private float jumpPower = 10f;
    [SerializeField]private float turnSmoothTime = 0.05f; //La velocidad a la que rota el mono, para que no sea tan brusco.
    private float _currentVelocity;

    private float _gravity = -9.81f;
    [SerializeField]private float gravityMultiplier = 3.0f;
    private float _velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();
    }
    private void ApplyRotation()//Esto rota al personaje hacia la direccion del Move 
    {
        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    public void ApplyMovement() //Aqui se aplica los inputs para mover al personaje
    {
        controller.Move(_direction * speed * Time.deltaTime);
    }
    public void ApplyGravity()
    {
        if (IsGrounded() && _velocity <0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity; //Se aplicara velocity para hacer que el jugador vaya hacia abajo.
    }
    public void Move(InputAction.CallbackContext context)//Logica para mover al personaje.
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);//Se usan X y Y porque es un _input de 2 Variables.
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started) return;
        if (!IsGrounded()) return;

        _velocity += jumpPower;
    }
    private bool IsGrounded() => controller.isGrounded;
}
