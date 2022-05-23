using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<CharacterController>().isGrounded)
        {

            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {

            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
