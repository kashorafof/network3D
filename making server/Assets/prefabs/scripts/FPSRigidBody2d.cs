using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRigidBody2d : MonoBehaviour
{
    Rigidbody rb;
    
    private Vector3 inputVector;
    bool sprint;
    [SerializeField] private float Speed;
    private float multiplier = 1f;
    

    [SerializeField] private GameObject orientation;

    private float distToGround;
    CapsuleCollider collider;
    private bool crouch;
    private float yRotate;


    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<CapsuleCollider>();

        //allMovementsDelegate += getInput;
        // allMovementsDelegate += Move;


    }


    private void FixedUpdate()
    {
        //allMovementsDelegate();
        getInput();
        if (inputVector.magnitude > 0.1f)
        {
            Rotate();
            //Move();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    [SerializeField] float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private void Rotate()
    {
        yRotate = Mathf.Atan2(inputVector.x, inputVector.z)*Mathf.Rad2Deg;

        
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, yRotate, ref turnSmoothVelocity, turnSmoothTime);
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;

       // Move(moveDirection.normalized * Speed * Time.fixedDeltaTime);
        animator.SetFloat("Turn" , Mathf.Atan2(inputVector.x, inputVector.z));
        animator.SetFloat("Forward", inputVector.magnitude);
    }

    void getInput()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        inputVector = new Vector3(Horizontal, 0f, Vertical);
        sprint = Input.GetKey(KeyCode.LeftShift);
        crouch = Input.GetKey(KeyCode.LeftControl);

    }



    void Move(Vector3 vector)
    {
        if (sprint) multiplier = 1.5f;
        else if (crouch) multiplier = 0.5f;
        else multiplier = 1f;

        rb.velocity = (vector);

        //rb.velocity = (orientation.transform.forward * inputVector.z * Speed * Time.deltaTime * multiplier) + (orientation.transform.right * inputVector.x * speed * Time.deltaTime * multiplier) + rb.velocity.y * Vector3.up;
        
        /*
        rb.AddRelativeForce(inputVector * speed * Time.deltaTime);

        rb.AddForce(orientation.transform.forward * inputVector.z * speed * Time.deltaTime * multiplier);
        rb.AddForce(orientation.transform.right * inputVector.x * speed * Time.deltaTime * multiplier);
        */
    }
    
}
