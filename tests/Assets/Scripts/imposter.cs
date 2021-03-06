using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imposter : MonoBehaviour
{
    public PlayerManagerAdd playerManager;
    public Material mat;
    public GameObject lastGameObject;
    public bodyManager nearestBody;


    private void Start()
    {
        playerManager = GetComponent<PlayerManagerAdd>();
    }

    private void Update()
    {

        if (playerManager != null)
        {
            if (playerManager.nearestPlayer != null && playerManager.nearestPlayer.GetComponent<PlayerManagerAdd>().stillAlive)
            {
                nearestBody = playerManager.nearestPlayer.GetComponent<bodyManager>();
                nearestBody.setBoundBodyColor( Color.red);

                if (Input.GetKeyDown(KeyCode.E))
                {
                   clientManager.attack(gameObject, playerManager.nearestPlayer);

                }
                if (lastGameObject != null && lastGameObject != playerManager.nearestPlayer)
                {
                    lastGameObject.GetComponent<bodyManager>().setBoundBodyColor( Color.black);
                }
                lastGameObject = playerManager.nearestPlayer;
            }
            else if (lastGameObject != null)
            {
                lastGameObject.GetComponent<bodyManager>().setBoundBodyColor(Color.black);
                lastGameObject = null;
            }
            else { nearestBody = null; }
        }
    }

}
