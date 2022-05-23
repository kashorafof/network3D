using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server
{
    public static int maxPlayer { get; private set; }

    public static bool gameStarted;

    public static int Port { get; private set; }
    public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
    public delegate void PacketHandler(int _fromClinet, Packet _packet);
    public static Dictionary<int, PacketHandler> packetHandlers;

    private static TcpListener tcpListener;
    private static UdpClient udpListener;

    public static void Start(int _maxPlayers, int _port)
    {
        maxPlayer = _maxPlayers;
        Port = _port;

        Debug.Log("Starting server ....");
        initializeServerData();
        tcpListener = new TcpListener(IPAddress.Any, Port);
        tcpListener.Start();

        udpListener = new UdpClient(Port);
        udpListener.BeginReceive(UDPReaceiveCallback, null);

        tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
        Debug.Log($"Server start on {Port}.");
        gameStarted = false;
    }

    public static int numberOfPlayers()
    {
        int numberOfPlayersINT =0;
        foreach (KeyValuePair<int,Client> client in clients)
        {
            if (client.Value.tcp.socket != null) numberOfPlayersINT++;
        }
        return numberOfPlayersINT;
    }

    private static void TCPConnectCallback(IAsyncResult _result)
    {
        TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
        tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

        Debug.Log($"Incomming connect from {_client.Client.RemoteEndPoint}");
        for (int i = 1; i <= maxPlayer; i++)
        {
            if (clients[i].tcp.socket == null)
            {
                clients[i].tcp.Connect(_client);
                return;
            }
        }
        Debug.Log($"{_client.Client.RemoteEndPoint} failed to connect: Server full!");
    }

    private static void UDPReaceiveCallback(IAsyncResult _result)
    {
        try
        {
            IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] _data = udpListener.EndReceive(_result, ref _clientEndPoint);
            udpListener.BeginReceive(UDPReaceiveCallback, null);
            if (_data.Length < 4)
            {
                return;
            }

            using (Packet _packet = new Packet(_data))
            {
                int _clientId = _packet.ReadInt();
                if (_clientId == 0)
                    return;
                if (clients[_clientId].udp.endPoint == null)
                {

                    clients[_clientId].udp.Connect(_clientEndPoint);
                    return;
                }

                if (clients[_clientId].udp.endPoint.ToString() == _clientEndPoint.ToString())
                {
                    clients[_clientId].udp.HandleData(_packet);
                }
            }
        }
        catch (Exception _ex)
        {
            Debug.Log($"Error receiving UDP data: {_ex}");
        }
    }

    public static void SendUDPData(IPEndPoint _clientEndPoint, Packet _packet)
    {
        try
        {
            if (_clientEndPoint != null)
                udpListener.BeginSend(_packet.ToArray(), _packet.Length(), _clientEndPoint, null, null);

        }
        catch (Exception _ex)
        {
            Debug.Log($"Error sending data to {_clientEndPoint} via UDP:{_ex}");
        }

    }



    public static void initializeServerData()
    {
        for (int i = 1; i <= maxPlayer; i++)
        {
            clients.Add(i, new Client(i));
        }

        packetHandlers = new Dictionary<int, PacketHandler>()
            {
                {(int) ClientPackets.welcomeReceived , ServerHandle.WelcomeRecieved },
                {(int) ClientPackets.playerMovement , ServerHandle.playerMovement },
                {(int) ClientPackets.cameraRotation , ServerHandle.cameraRotation },
                {(int) ClientPackets.MasterServerConnected , ServerHandle.SendData },
                {(int) ClientPackets.Attack , ServerHandle.PerformAttack },
                {(int) ClientPackets.Report , ServerHandle.RecieveReport },
                {(int) ClientPackets.SendVote , ServerHandle.RecieveVote },
            };
        Debug.Log("Initialize packets.");
    }



    public static void startGame()
    {
        gameStarted = true;
        Debug.Log("game Started");
    }


    public static void Stop()
    {
        tcpListener.Stop();
        udpListener.Close();

    }
}
