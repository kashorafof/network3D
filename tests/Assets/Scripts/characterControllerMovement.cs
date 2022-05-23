using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControllerMovement : MonoBehaviour
{
    float horizontal;
    float vertical;
    Vector3 move;
    [SerializeField] Transform cam;
    CharacterController controller = new CharacterController();

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        
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
            controller.Move(moveDirection.normalized * Speed * Time.deltaTime);
        }
    }

}
