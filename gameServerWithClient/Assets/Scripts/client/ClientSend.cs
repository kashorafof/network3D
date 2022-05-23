using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        clientToMasterServer.instance.tcp.SendData(_packet);
    }
    
    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        clientToMasterServer.instance.udp.SendData(_packet);
    }

    #region packets
    public static void ServerHasBeenMade()
    {
        using (Packet _packet = new Packet((int)ClientPackets.ServerHasBeenMade))
        {
            _packet.Write(clientToMasterServer.instance.myId);
            _packet.Write(NetworkManager.port);
            _packet.Write(NetworkManager.MaxPlayers);
            
            SendTCPData(_packet);
        }
    }
    #endregion
    

    public static void SendServerStatus()
    {
        using (Packet _packet = new Packet((int)ClientPackets.SendServerStatus))
        {
            _packet.Write(clientToMasterServer.instance.myId);
            _packet.Write(NetworkManager.port);
            _packet.Write(NetworkManager.MaxPlayers);
            _packet.Write(Server.numberOfPlayers());
            _packet.Write(Server.gameStarted);

            SendUDPData(_packet);
            Debug.Log("data sended");
        }
    }
}
