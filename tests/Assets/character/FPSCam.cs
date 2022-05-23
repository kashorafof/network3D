using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    [SerializeField] GameObject headBone ,orientation;

    [SerializeField] private float maxCameraRotationX = 80f , maxCameraRotationY = 30f;
    [SerializeField] float SenX, SenY;
    private float xRotation , yRotation , locRotY;

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
        xRotation = Mathf.Clamp(xRotation, -maxCameraRotationY, maxCameraRotationY +7f);

        locRotY = transform.localEulerAngles.y;
        if (locRotY >= 180)
            locRotY -= 360;
        //if ( -70f <= locRotY && locRotY <= 70f)
        yRotation = locRotY + Input.GetAxis("Mouse X") * SenX * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, -maxCameraRotationX, maxCameraRotationX);

        transform.localRotation= Quaternion.Euler(xRotation, yRotation, 0f);

        orientation.transform.localRotation = Quaternion.Euler(0f, yRotation , 0f);

        headBone.transform.localRotation =transform.localRotation;
        
    }
}
