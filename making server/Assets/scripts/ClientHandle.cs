using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        if (_packet.ReadBool())
        {
            Debug.Log("disconnecting from server");
            client.instance.Disconnect();

            Debug.Log("connect to master server");
            client.instance.port = client.instance.mainServerPort;
            client.instance.ConnectToServer();
            return;
        }

        GameManager.instance.votingTime = _packet.ReadInt();
        Debug.Log($"message from Server: {msg}");
        client.instance.myId = _myId;
        // send wlcome recieve packet
        ClientSend.WelcomeReceived();
        client.instance.udp.Connect(((IPEndPoint)client.instance.tcp.socket.Client.LocalEndPoint).Port);

    }
    public static bool serverStarted;
    public static void checkServer(Packet _packet)
    {
        serverStarted = _packet.ReadBool();
        if (!serverStarted)
        {

        }
    }


    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id,_username,_position,_rotation );

    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        GameManager.players[_id].transform.position = _position;
    }

    public static void changeAnimation(int id, bool moving)
    {
        Animator animator = GameManager.players[id].animator;
        animator.SetBool("move", moving);
    }

    public static void changeAnimation( int id,bool moving , bool crouch)
    {
       Animator animator= GameManager.players[id].animator;
       animator.SetBool("move", moving);
       animator.SetBool("crouch", crouch);
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();
        GameManager.players[_id].transform.rotation = _rotation;

    }

    public static void CameraRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _cameraRotation = _packet.ReadQuaternion();
        GameManager.players[_id].cam.transform.rotation = _cameraRotation;

    }

    public static void recieveStatus(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float multiplier = _packet.ReadFloat();
        bool isMoving = _packet.ReadBool();

        Animate(_id, multiplier, isMoving);

    }
    public static void Animate(int id , float multiplier , bool isMoving)
    {
        Animator animator = GameManager.players[id].animator;

        if (multiplier == 0)
        {
            animator.SetBool("Base Layer.crouch", true);
        }
        else if (isMoving)
        {
            animator.StopPlayback();
            animator.Play("Base Layer.moving");
        }
        else
        {
            animator.StopPlayback();
            animator.Play("Base Layer.idle");
        }
            

    }


    public static void PerformeAttack(Packet _packet)
    {
        Debug.Log("trying to performe the attack");
        int attackerId = _packet.ReadInt();
        int secondPlayerId = _packet.ReadInt();

        GameObject attackerPlayer = GameManager.players[attackerId].gameObject;
        GameObject secondPlayer = GameManager.players[secondPlayerId].gameObject;

        attackerPlayer.transform.LookAt(secondPlayer.transform);
        secondPlayer.GetComponent<PlayerManager>().Die();
        Instantiate(GameManager.instance.bloodSplatter, secondPlayer.transform.position + Vector3.up *0.01f, Quaternion.Euler(Vector3.right * -90));

        Debug.Log("attack performed succ");
    }

    public static void PerformeReport(Packet _packet)
    {
        int reporterId = _packet.ReadInt();
        GameManager.instance.Report(reporterId);
    }
    
    public static void finishVoting(Packet _packet)
    {
        GameManager.instance.ReportSystem.SetActive(false);
        GameManager.instance.changePlayAbility();
    }


    public static void removePlayer(Packet _packet) {
        int id = _packet.ReadInt();

        Destroy( GameManager.players[id].gameObject);
        GameManager.players.Remove(id);
    }
}
