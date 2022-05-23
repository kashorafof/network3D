using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.NetworkInformation;

public class NetworkManager : MonoBehaviour
{
    #region singelton
    public static NetworkManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("instance already exists, destroying object!");
            Destroy(this);
        }

    }
    #endregion

    public GameObject playerPrefab;
    public GameObject votingManager;

    public static int MaxPlayers = 1;
    public static int port;

    string portFilePath ;
    string[] lines;

    private void Start()
    {
#if UNITY_EDITOR
        portFilePath = @"D:\among us project\port.txt";
#else
        portFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\port.txt";
#endif
        lines = File.ReadAllLines(portFilePath);
        port = int.Parse(lines[0]);
        MaxPlayers = int.Parse(lines[1]);

        Debug.Log("server data path is: " + portFilePath);
        Server.Start(MaxPlayers, port);
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }

    public Player InstantiatePlayer()
    {
        return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(port + "is used: " + PortInUse(port));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Server.startGame();
        }
    }

    public static bool PortInUse(int port)

    {

        bool inUse = false;



        IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();

        IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();




        foreach (IPEndPoint endPoint in ipEndPoints)

        {

            if (endPoint.Port == port)

            {

                inUse = true;

                break;

            }

        }




        return inUse;

    }

}
