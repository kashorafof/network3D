using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    //Rigidbody rb;
    public GameObject orientation , cam , headBone;
    [SerializeField] float speed;
    float multiplier;
    private Vector3 inputVector;
    private bool jumping;
    private bool sprint;
    private bool crouch;
    
    

    Animator animator;
    private Vector3 prevInput;

    // Start is called before the first frame update
    void Start()
    {
       // rb = GetComponent<Rigidbody>();
        animator = GameManager.players[client.instance.myId].GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        Move();


    }

    private void LateUpdate()
    {

        headBone.transform.localRotation = cam.transform.localRotation;
    }

    void Move()
    {
        if (crouch) { multiplier = 0f; }
        else if (sprint) { multiplier = 1.5f; }
        else { multiplier = 1f; }

        if (inputVector.magnitude >= 0.1f)
        {
            //rb.velocity = (orientation.transform.forward * inputVector.z * speed * Time.deltaTime * multiplier) + (orientation.transform.right * inputVector.x * speed * Time.deltaTime * multiplier) + rb.velocity.y * Vector3.up;
            //rb.AddForce((orientation.transform.forward * inputVector.z * speed * Time.deltaTime * multiplier) + (orientation.transform.right * inputVector.x * speed * Time.deltaTime * multiplier) + rb.velocity.y * Vector3.up);
            transform.rotation = orientation.transform.rotation;
            cam.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x,0f,0f);

        }
        prevInput = inputVector;
    }

    void getInput()
    {

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        inputVector = new Vector3(Horizontal, 0f, Vertical);
        sprint = Input.GetKey(KeyCode.LeftShift);
        crouch = Input.GetKey(KeyCode.LeftControl);
    }

}
