using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    public static void WelcomeRecieved(int _fromClient, Packet _packet)
    {
        ServerSend.sendServerStatusToPlayers( _fromClient);
        if (Server.gameStarted){return;}


        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \" {_username}\" (ID : {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})");
        }

        Server.clients[_fromClient].SentIntoGame(_username);
        
    }

    public static void SendData(int _fromClient, Packet _packet)
    {
        string msg = _packet.ReadString();
        Debug.Log(msg);
    }

    public static void playerMovement(int _fromClient, Packet _packet)
    {
        Vector2 MovementInputs = _packet.ReadVector2();
        bool _isCrouching = _packet.ReadBool();
        bool _isSprinting = _packet.ReadBool();
        Quaternion _rotation = _packet.ReadQuaternion();


        Server.clients[_fromClient].player.Move(MovementInputs, _isCrouching, _isSprinting);
        Server.clients[_fromClient].player.transform.rotation = _rotation;

    }

    public static void cameraRotation(int _fromClient, Packet _packet)
    {
        Quaternion _camRotation = _packet.ReadQuaternion();

        Server.clients[_fromClient].player.cam.transform.rotation = _camRotation;
        ServerSend.CameraRotation(Server.clients[_fromClient].player);
    }

    public static void PerformAttack(int _fromClient, Packet _packet)
    {
        Debug.Log("attack received");
        int attackerId = _packet.ReadInt();
        int secondPlayerId = _packet.ReadInt();

        GameObject attackerPlayer = Server.clients[attackerId].player.gameObject;
        GameObject secondPlayer = Server.clients[secondPlayerId].player.gameObject;

        Vector3 move = secondPlayer.transform.position - attackerPlayer.transform.position;
        attackerPlayer.GetComponent<CharacterController>().Move(new Vector3(move.x, attackerPlayer.transform.position.y, move.z));
        ServerSend.PlayerPosition(Server.clients[attackerId].player);
        ServerSend.PlayerRotation(Server.clients[attackerId].player);
        ServerSend.attackDetails(attackerId, secondPlayerId);

        Server.clients[secondPlayerId].player.ChangeCameraRotation(new Quaternion(0, 0, 0, 0));
    }

    public static void RecieveReport(int _fromClient, Packet _packet)
    {
        Debug.Log(Server.clients[_fromClient].player.name + "reported");
        ServerSend.SendReportToAll(_fromClient);
        
        foreach (KeyValuePair<int , Client> _client in Server.clients)
        {
            if (_client.Value.player != null)
            {
                _client.Value.player.ChangeCameraRotation(new Quaternion(0, 0, 0, 0));
                ServerSend.CameraRotation(_client.Value.player);
            }
        }

        NetworkManager.instance.votingManager.SetActive(true);
        Debug.Log("voting managet activerd");
    }


    public static void RecieveVote(int _fromClient, Packet _packet)
    {
        int VotedOnId = _packet.ReadInt();
        Debug.Log($"player with id: {_fromClient} voted on player with id: {VotedOnId}");

        votingManager.instance.voteOn(VotedOnId);
    }
}
