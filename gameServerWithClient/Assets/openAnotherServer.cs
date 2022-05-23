using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
public class openAnotherServer : MonoBehaviour
{
    private bool started;

    // Start is called before the first frame update
    void Start()
    {
        started = false;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!started)
        {
            Process.Start(@"C:\Users\kasho\Desktop\packup\gameServerWithClient\build\gameServerWithClient.exe");
            

            started = true;
        }
    }
}
