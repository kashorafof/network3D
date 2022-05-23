using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRigidBody : MonoBehaviour
{
    Rigidbody rb;

    public delegate void allMovements();
    allMovements allMovementsDelegate;

    private Vector3 inputVector;
    bool jumping, sprint;
    [SerializeField] private float speed;
    private float multiplier=1f;

    public float jumpHeight = 5f;
    private float jumpForce;

    [SerializeField] private GameObject orientation;

    private float distToGround;
    CapsuleCollider collider;
    private bool crouch;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<CapsuleCollider>();

        distToGround = collider.bounds.extents.y;

        //allMovementsDelegate += getInput;
       // allMovementsDelegate += Move;


    }


    private void Update()
    {
        //allMovementsDelegate();
        getInput();
        Move();
    }
    
    void getInput()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        inputVector = new Vector3(Horizontal, 0f, Vertical);
        jumping = Input.GetKeyDown(KeyCode.Space);
        sprint = Input.GetKey(KeyCode.LeftShift);
        crouch = Input.GetKey(KeyCode.LeftControl);
        
    }

    

    void Move()
    {
        if (sprint) multiplier = 1.5f;
        else if (crouch) multiplier = 0.5f;
        else multiplier = 1f;

        rb.velocity=(orientation.transform.forward * inputVector.z * speed * Time.deltaTime * multiplier ) + (orientation.transform.right * inputVector.x * speed * Time.deltaTime * multiplier) + rb.velocity.y*Vector3.up;

        if (jumping && isGrounded())
        {
            jumpForce = Mathf.Sqrt(Math.Abs( 2 * jumpHeight * Physics.gravity.y));
            rb.AddForce(Vector3.up * jumpForce , ForceMode.Impulse);
        }
        /*
        rb.AddRelativeForce(inputVector * speed * Time.deltaTime);

        rb.AddForce(orientation.transform.forward * inputVector.z * speed * Time.deltaTime * multiplier);
        rb.AddForce(orientation.transform.right * inputVector.x * speed * Time.deltaTime * multiplier);
        */
    }

    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
