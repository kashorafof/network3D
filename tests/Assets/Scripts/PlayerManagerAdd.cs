using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerAdd : MonoBehaviour
{
    public List<GameObject> nearPlayers;
    public GameObject nearestPlayer;

    public CharacterController controller;
    public Material mat;
    public bodyManager body;

    public bool isImposter;
    public bool stillAlive;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        body = GetComponent<bodyManager>();
        stillAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (nearPlayers.Count>0)
        {
            float nearestDistance =100f;
            foreach (GameObject _player in nearPlayers)
            {
                float distance = (transform.position - _player.transform.position).magnitude;
                if(distance < nearestDistance)
                {
                    nearestPlayer = _player;
                    nearestDistance = distance;
                }
                
            }
        }
        else
        {
            nearestPlayer = null;
        }

        /*
        if (nearKilledPlayer.Count >0)
        {
            killedPlayer = nearKilledPlayer[0];
            killedPlayer.GetComponent<bodyManager>().SetColor("Color_282E291D", Color.white);
            lastKilledPlayer = killedPlayer;
        }
        else if (lastKilledPlayer != killedPlayer)
        {
            lastKilledPlayer.GetComponent<bodyManager>().SetColor("Color_282E291D", Color.black);
        }
        else killedPlayer = null;
       */

    }
    
    public void Die()
    {
        body.head.SetActive(false);
        body.bloodPlane.SetActive(true);
        //GetComponent<characterControllerMovement>().enabled = false;
        stillAlive = false;
    }
}
