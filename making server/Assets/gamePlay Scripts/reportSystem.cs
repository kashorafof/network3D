using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reportSystem : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject killedPlayer;
    public GameObject lastKilledPlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        checkingKilledPlayers();

        if (killedPlayer != null && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("reported");
            ClientSend.Report();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
        }

    }

    void checkingKilledPlayers()
    {
        foreach (GameObject _player in playerManager.nearPlayers)
        {
            if (!_player.GetComponent<PlayerManager>().stillAlive)
            {
                killedPlayer = _player;
                break;
            }
            killedPlayer = null;
        }

        if (playerManager.nearPlayers.Count == 0)
        {
            killedPlayer = null;
        }

        if (lastKilledPlayer != killedPlayer && lastKilledPlayer != null)
            lastKilledPlayer.GetComponent<bodyManager>().setBoundBodyColor(Color.black);

        if (killedPlayer != null)
        {
            killedPlayer.GetComponent<bodyManager>().setBoundBodyColor(Color.white);
            lastKilledPlayer = killedPlayer;
        }
    }

}
