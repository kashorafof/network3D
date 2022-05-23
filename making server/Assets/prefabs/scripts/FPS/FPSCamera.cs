using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField] Transform head, body;
    float mouseX, mouseY;

    //sensitivity
    [SerializeField] float SensitivityX =50f , SensitivityY=50f;
    private float xRotation;

    [SerializeField] float min=-40f, max=70f;

    public float smooth = 1.5f;            // The relative speed at which the camera will catch up.


    private Transform player;            // Reference to the player's transform.
    private Vector3 relCameraPos;        // The relative position of the camera from the player.
    private float relCameraPosMag;        // The distance of the camera from the player.
    private Vector3 newPos;                // The position the camera is trying to reach.


    void refreshPos()
    {

        // Setting the relative position as the initial relative position of the camera in the scene.
        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        refreshPos();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Rotate();
        refreshPos();
        checkLerp();
    }

    void Rotate()
    {
        mouseX = Input.GetAxis("Mouse X") * SensitivityX * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * SensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, min, max);
        head.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        body.Rotate(Vector3.up * mouseX);
    }

    void checkLerp()
    {

        // The standard position of the camera is the relative position of the camera from the player.
        Vector3 standardPos = player.position + relCameraPos;

        // The abovePos is directly above the player at the same distance as the standard position.
        Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;

        // An array of 5 points to check if the camera can see the player.
        Vector3[] checkPoints = new Vector3[5];

        // The first is the standard position of the camera.
        checkPoints[0] = standardPos;

        // The next three are 25%, 50% and 75% of the distance between the standard position and abovePos.
        checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
        checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);

        // The last is the abovePos.
        checkPoints[4] = abovePos;

        // Run through the check points...
        for (int i = 0; i < checkPoints.Length; i++)
        {
            // ... if the camera can see the player...
            if (ViewingPosCheck(checkPoints[i]))
                // ... break from the loop.
                break;
        }

        // Lerp the camera's position between it's current position and it's new position.
        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
    }



    void SmoothLookAt()
    {
        // Create a vector from the camera towards the player.
        Vector3 relPlayerPosition = player.position - transform.position;

        // Create a rotation based on the relative position of the player being the forward vector.
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);

        // Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }

    bool ViewingPosCheck(Vector3 checkPoint)
    {
        RaycastHit hit;
        if(Physics.Raycast(checkPoint , player.position-checkPoint , out hit , relCameraPosMag))
            if (hit.collider.tag != "Player")
                return false;

        newPos = checkPoint;
        return true;
    }
}
