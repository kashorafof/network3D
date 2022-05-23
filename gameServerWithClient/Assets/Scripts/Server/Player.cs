using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;

    public bool isImposter;
    public bool stillAlive;

    private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
    private bool[] inputs;
    public FPSController Controller;
    public CharacterController _controller;

    public GameObject orientation, cam;


    public Quaternion cameraRotation;
    private bool isMoving;
    private bool crouch;
    private bool sprint;
    private float multiplier;
    float speedY;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        stillAlive = true;
    }

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
    }

    private void Update()
    {
        if (_controller.isGrounded)
            speedY = 0;
        else
            speedY -= 9.8f;
    }

    /*
    private void Move(Vector2 _inputsDirection)
    {
            Vector3 _moveDirection = transform.right * _inputsDirection.x + transform.forward * _inputsDirection.y;
            transform.position += _moveDirection * moveSpeed;

            ServerSend.PlayerPosition(this, isMoving);
            ServerSend.PlayerRotation(this);
    }
    */

    public void Move(Vector2 inputs , bool _crouching , bool _sprinting)
    {

        multiplier = ServerSettings.instance.speedMultiplier;
        float _speed = ServerSettings.instance.speed;

        bool _isMoving;
        if (_crouching) { multiplier = 0f; }
        else { multiplier = 1f; }
        _isMoving = false;
        

        if (inputs.magnitude >= 0.1f)
        {
            if (_sprinting) { multiplier = 1.5f; }
            _isMoving = true;
            _controller.Move( (((transform.right * inputs.x ) + (transform.forward * inputs.y)  ) * _speed * multiplier + Vector3.up*speedY)*Time.deltaTime);
        }
        ServerSend.sendStatus(this ,multiplier , _isMoving );
        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

    internal void SetInput(bool[] _inputs, Quaternion _rotation)
    {
         inputs = _inputs;
         transform.rotation = _rotation;
    }
     
    public void ChangeCameraRotation(Quaternion _camRotation)
    {
         cam.transform.rotation = _camRotation;
         ServerSend.CameraRotation(this);
    }
}

