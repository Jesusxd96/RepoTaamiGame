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
    #region Movimiento
    private CharacterController _characterController;
    private Vector2 _input;//Input del jugador. 
    private Vector3 _direction;
    [SerializeField]private float speed = 6f; //La velocidad a la que se mueve el personaje
    #endregion
    #region Saltos
    [SerializeField]private float jumpPower = 10f;
    private int _numberOfJumps=0;
    [SerializeField] private int maxNumberOfJumps = 2;//Numero maximo de saltos, apenas veremos si hay o no doble salto
    #endregion
    #region Rotacion
    [SerializeField]private float rotationSpeed = 500.0f; //La velocidad a la que rota el mono, para que no sea tan brusco.
    private Camera _mainCamera;
    #endregion
    #region Gravedad
    private float _gravity = -9.81f; //Gravedad
    [SerializeField]private float gravityMultiplier = 3.0f;
    private float _velocity;
    #endregion
    #region Setters y Getters
    public float Speed
    {
        get => speed;
    }
    public void SetSpeed(float value)
    {
        speed = value;
    }
    public float JumpPower
    {
        get => jumpPower;
    }
    public void SetJumpPower(float value)
    {
        jumpPower = value;
    }
    public int NumOfJumps
    {
        get => maxNumberOfJumps;
    }
    public void SetMaxNumberOfJumps(int value)
    {
        maxNumberOfJumps = value;
    }
    public float RotationSpeed
    {
        get => rotationSpeed;
    }
    public void SetRotationSpeed(float value)
    {
        rotationSpeed = value;
    }
    public float GravityMultiple
    {
        get => gravityMultiplier;
    }
    public void SetGravityMultiplier(float value)
    {
        gravityMultiplier = value;
    }
    #endregion
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }
    private void ApplyRotation()//Esto rota al personaje hacia la direccion del Move 
    {
        if (_input.sqrMagnitude == 0) return;

        _direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(_input.x, 0.0f, _input.y);
        var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    public void ApplyMovement() //Aqui se aplica los inputs para mover al personaje
    {
        _characterController.Move(_direction * speed * Time.deltaTime);
    }
    public void ApplyGravity()
    {
        if (IsGrounded() && _velocity <0.0f)//Si el personaje esta en el piso y la velocidad es menor a 0
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
        if (!context.started) return;
        //if (!IsGrounded()) return; //Solo un salto
        if (!IsGrounded() && _numberOfJumps >= maxNumberOfJumps) return;//Doble salto
        if (_numberOfJumps == 0) StartCoroutine(WaitForLanding());

        _numberOfJumps++;//Se aumenta el contador de saltos.
        //_velocity = jumpPower; // Altura y fuerza constante a la hora de saltar.
        _velocity = jumpPower / _numberOfJumps; //Con cada salto disminuye la altura/fuerza de este.
    }

    private IEnumerator WaitForLanding() {//Corrutina
        yield return new WaitUntil(() => !IsGrounded());//Se espera a que el personaje no este grounded, osease ha saltado.
        yield return new WaitUntil(IsGrounded);
        _numberOfJumps = 0;
    }
    private bool IsGrounded() => _characterController.isGrounded;//Regresa una boolean para simplemente llamar IsGrounded en vez de toda la linea
}
