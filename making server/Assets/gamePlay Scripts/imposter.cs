using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imposter : MonoBehaviour
{
    public PlayerManager playerManager;
    public bodyManager nearestBody;
    public GameObject lastGameObject;
    public float killColdown;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {

        if (playerManager != null)
        {
            if (playerManager.nearestPlayer != null && playerManager.nearestPlayer.GetComponent<PlayerManager>().stillAlive)
            {
                nearestBody = playerManager.nearestPlayer.GetComponent<bodyManager>();
                nearestBody.setBoundBodyColor(Color.red);
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ClientSend.attack(gameObject, playerManager.nearestPlayer);

                }
                if (lastGameObject != null && lastGameObject != playerManager.nearestPlayer)
                {
                    lastGameObject.GetComponent<bodyManager>().setBoundBodyColor( Color.black);
                }
                lastGameObject = playerManager.nearestPlayer;
            }
            else if (lastGameObject != null)
            {
                lastGameObject.GetComponent<bodyManager>().setBoundBodyColor( Color.black);
                lastGameObject = null;
            }
            else { nearestBody = null; }
        }
    }


}
