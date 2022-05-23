using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    [SerializeField] GameObject headBone ,orientation;

    [SerializeField] float SenX, SenY;
    public float xRotation , yRotation;
    public float locRotY;
  
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        xRotation -= Input.GetAxis("Mouse Y") * SenY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -30f, 37f);

        locRotY = transform.localEulerAngles.y;
        if (locRotY >= 180)
            locRotY -= 360;
        //if ( -70f <= locRotY && locRotY <= 70f)
        yRotation = locRotY + Input.GetAxis("Mouse X") * SenX * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, -60f, 60f);

        transform.localRotation= Quaternion.Euler(xRotation, yRotation, 0f);

        orientation.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        
        
    }
}
