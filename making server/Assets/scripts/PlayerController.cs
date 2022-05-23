using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private void FixedUpdate()
    {
        if (GameManager.instance.playAble)
        {
            SendInputToServer();
            ClientSend.CameraRotation();
        }
    }

    private void SendInputToServer()
    {
        
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        Vector2 MovementInputs = new Vector2(Horizontal, Vertical);

        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        

       ClientSend.PlayerMovement(MovementInputs, sprint, crouch);

    }


}
