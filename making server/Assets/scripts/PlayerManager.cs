using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public GameObject cam;
    public Animator animator;
    
    public bool isImposter;
    public bool stillAlive;


    public List<GameObject> nearPlayers = new List<GameObject>();
    public GameObject nearestPlayer;

    public CharacterController controller;
    public Material mat;
    public bodyManager body;

    public Color mainColor;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        body = GetComponent<bodyManager>();
        stillAlive = true;
        
    }

    void Update()
    {

        checkingNearestPlayer();


    }
    

    public void Die()
    {
        stillAlive = false;
        body.head.SetActive(false);
        body.bloodPlane.SetActive(true);
        if (id == client.instance.myId)
        {
            GetComponent<PlayerController>().enabled = false;
            cam.SetActive(false);
        }
    }

    public void checkingNearestPlayer()
    {
        if (nearPlayers.Count > 0)
        {
            float nearestDistance = 100f;
            foreach (GameObject _player in nearPlayers)
            {
                float distance = (transform.position - _player.transform.position).magnitude;
                if (distance < nearestDistance)
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

    }
}
