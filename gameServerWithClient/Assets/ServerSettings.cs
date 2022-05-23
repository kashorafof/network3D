using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSettings : MonoBehaviour
{
    #region singelton
    public static ServerSettings instance;
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

    public int VotingTime = 10;

    public int speedMultiplier = 1;
    public float speed = 15;


    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
