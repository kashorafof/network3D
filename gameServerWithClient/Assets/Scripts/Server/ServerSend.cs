using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSend 
{
    private static int sendAttackDetails;
    #region sendTCPData
    private static void SendTCPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    private static void SendTCPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.maxPlayer; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }
    private static void SendTCPDataToAll(int _expectClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.maxPlayer; i++)
        {
            if (i != _expectClient)
                Server.clients[i].tcp.SendData(_packet);
        }
    }
    #endregion

    #region sendUDPData
    private static void SendUDBData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }
    private static void SendUDPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.maxPlayer; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }
    private static void SendUDPDataToAll(int _expectClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.maxPlayer; i++)
        {
            if (i != _expectClient)
                Server.clients[i].udp.SendData(_packet);
        }
    }
    #endregion


    public static void Welcome(int _toClinet, string _msg)
    {
        using (Packet _packet = new Packet((int)ServerPackets.welcome))
        {
            _packet.Write(_msg);
            _packet.Write(_toClinet);
            _packet.Write(Server.gameStarted);
            _packet.Write(ServerSettings.instance.VotingTime);

            SendTCPData(_toClinet, _packet);
        }
    }

    public static void SpawnPlayer(int _toClient, Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.username);
            _packet.Write(_player.transform.position);
            _packet.Write(_player.transform.rotation);
            _packet.Write(_player.isImposter);

            SendTCPData(_toClient, _packet);
        }

    }

    public static void PlayerPosition(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.transform.position);
            SendUDPDataToAll(_packet);
        }
    }
    
    internal static void PlayerRotation(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.transform.rotation);
            SendUDPDataToAll(_player.id, _packet);
        }
    }

    public static void CameraRotation(Player _player)
    {
        using (Packet _packet = new Packet((int)ServerPackets.cameraRotation))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.cam.transform.rotation);
            
            SendUDPDataToAll(_player.id, _packet);
        }
    }

    public static void sendStatus ( Player _player ,float multiplier , bool _isMoving)
    {
        using(Packet _packet = new Packet((int)ServerPackets.sendStatus))
        {
            _packet.Write(_player.id);
            _packet.Write(multiplier);
            _packet.Write(_isMoving);
            SendUDPDataToAll(_packet);
        }
    }

    public static void attackDetails(int attackerId , int secondPlayerId)
    {
        using (Packet _packet = new Packet((int)ServerPackets.SendAttackDetails))
        {
            _packet.Write(attackerId);
            _packet.Write(secondPlayerId);
            SendTCPDataToAll(_packet);
        }
        Debug.Log("attack status send");
    }

    public static void playerDisconnected(int _id)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerDisconnect))
        {
            _packet.Write(_id);
            SendTCPDataToAll(_packet);
        };

    }

    public static void SendReportToAll(int reporterId)
    {
        using(Packet _packet = new Packet ((int)ServerPackets.PerformeReport))
        {
            _packet.Write(reporterId);
            SendTCPDataToAll(_packet);
        }
    }

    public static void sendServerStatusToPlayers(int toPlayer)
    {
        using (Packet _packet = new Packet((int)ServerPackets.sendServerStatusToPlayer))
        {
            _packet.Write(Server.gameStarted);
            SendTCPData(toPlayer, _packet);
        }
    }

    public static void finishVoting()
    {
        using(Packet _packet = new Packet ((int)ServerPackets.finishVoting))
        {


            SendTCPDataToAll(_packet);
        }
    }
}
