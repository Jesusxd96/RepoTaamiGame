using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*For testing purposes we are using the old input system, once it moves we will be moving from this to the new one*/
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f; //La velocidad a la que se mueve el personaje
    
    public float turnSmoothTime = 0.1f; //La velocidad a la que rota el mono, para que no sea tan brusco.
    float turnSmoothVelocity;

    private void Update()
    {   /*We get the inputs here*/
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if(direction.magnitude >= 0.1f) /*Here is for the rotation.*/
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg+ cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            /*This is to move while taking into consideration the camera.*/
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
    /*
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, forceMagnitude, 0), ForceMode.VelocityChange);
    }*/
}
