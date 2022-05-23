using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class votingManager : MonoBehaviour
{
    #region singelton
    public static votingManager instance;
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



    Dictionary<int, int> playersStillAliveId = new Dictionary<int, int>();
    
    public int highest_id;
    public bool tie;
    void OnEnable()
    {
        highest_id = 0;
        tie = true;
        foreach (KeyValuePair<int, Client> _client in Server.clients)
        {
            if (_client.Value.player != null)
            {
                if (_client.Value.player.stillAlive)
                {
                    playersStillAliveId.Add(_client.Key, 0);
                }
            }
        }
        Invoke("finishVoting", ServerSettings.instance.VotingTime + 1.5f);
    }

    public void voteOn(int id)
    {
        playersStillAliveId[id]++;
        if(highest_id == 0)
        {
            tie = false;
            highest_id = id;
        }
        else if(playersStillAliveId[id] > playersStillAliveId[highest_id])
        {
            tie = false;
            highest_id = id;
        }
        else if(playersStillAliveId[id] == playersStillAliveId[highest_id])
        {
            tie = true;
        }
    }


    void OnDisable()
    {
        playersStillAliveId.Clear();
    }

    public void finishVoting()
    {
        ServerSend.finishVoting();
        if (tie )
            Debug.Log("tie");
        else
            Debug.Log("highest player is: " + highest_id);
        instance.gameObject.SetActive(false);
    }
}
