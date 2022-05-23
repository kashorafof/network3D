using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject orientation;
    public GameObject cam;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if (Input.GetMouseButtonDown(2)) turn();
        
    }
    public float turnSmoothVelocity;
    public float turnSmoothTime;

    void move()
    {
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (inputs.magnitude >= 0.1f)
        {
            controller.Move((orientation.transform.right * speed * inputs.x + orientation.transform.forward * speed * inputs.y) * Time.deltaTime);
            turn();
        }

        if (!controller.isGrounded)
        {
            controller.Move(new Vector3(0, -9f, 0));
        }
    }

    public void turn()
    {

        transform.rotation = orientation.transform.rotation;
        cam.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x, 0f, 0f);
    }
}
