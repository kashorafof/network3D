using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidBodyCharacterController : MonoBehaviour
{
    float horizontal;
    float vertical;
    Vector3 move;
    [SerializeField] Transform cam;
    Rigidbody rb;
    [SerializeField] float jumpHeight;
    float jumpForce;
    // Update is called once per frame
    void Update()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Movement();


    }


    [SerializeField] float Speed;
    private float turnSpeed = 0.5f;
    [SerializeField] float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Move(moveDirection.normalized * Speed * Time.deltaTime);
        }
        else
        {

            rb.velocity = Vector3.zero + Vector3.up * rb.velocity.y;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
             jumpForce = Mathf.Sqrt(2 * jumpHeight*9.8f) ;
            rb.AddForce(Vector3.up* jumpForce , ForceMode.Impulse);
        }
    }

    void Move(Vector3 move)
    {
        rb.velocity = move + Vector3.up *rb.velocity.y;
        //transform.Translate(transform.InverseTransformDirection(move) );
    }


}
