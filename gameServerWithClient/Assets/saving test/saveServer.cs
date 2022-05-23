using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class saveServer {
    
    public static void SaveServer(serverData _server)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = @"C:\Users\kasho\Desktop\packup\gameServerWithClient\saved servers\servers.lala";
        FileStream stream = new FileStream(path , FileMode.Create);

        ServerDublicate data = new ServerDublicate(_server);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    

}
