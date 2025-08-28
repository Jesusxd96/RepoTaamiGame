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
    [SerializeField]private float jumpForce = 10f;
    [SerializeField]private float turnSmoothTime = 0.05f; //La velocidad a la que rota el mono, para que no sea tan brusco.
    private float _currentVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (_input.sqrMagnitude == 0) return;
        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);

        controller.Move(_direction*speed * Time.deltaTime);
    }
    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);//Se usan X y Y porque es un _input de 2 Variables.
    }
    public void Jump(InputAction.CallbackContext context)
    {

    }
    public void OldMovement()
    {
        /*//We get the inputs here
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if(direction.magnitude >= 0.1f) //Here is for the rotation.
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg+ cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //This is to move while taking into consideration the camera.
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        //Logica de Salto simple
        if ((Input.GetButton("Jump"))&&(isGrounded==true))//Se usa la variable interna en lo que se ve como hacerle para la de Controller.
        {
            rb.AddForce(transform.up * jumpForce);
            //rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
            //isGrounded = false;
        }*/
    }
}
