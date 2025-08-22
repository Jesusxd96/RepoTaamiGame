using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 inputDirection;
    private float forceMagnitude;//La velocidad del personaje pues.

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, forceMagnitude, 0), ForceMode.VelocityChange);
    }
}
