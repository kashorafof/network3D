using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ServerDublicate {
    public int ip;
    public int port;
    public int maxPlayers;

    public ServerDublicate(serverData _server)
    {
        ip = _server.ip;
        port = _server.port;
        maxPlayers = _server.maxPlayers;
    }
}
