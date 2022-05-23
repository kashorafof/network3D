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

        Debug.Log($"message from Server: {msg}");
        clientToMasterServer.instance.myId = _myId;
        // send wlcome recieve packet
        ClientSend.ServerHasBeenMade();
        clientToMasterServer.instance.udp.Connect(((IPEndPoint)clientToMasterServer.instance.tcp.socket.Client.LocalEndPoint).Port);
        clientToMasterServer.isConnect = true;
    }
    
}
