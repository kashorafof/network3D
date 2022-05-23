using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cardManager : MonoBehaviour
{
    int id;
    public TextMeshProUGUI name;
    public Image color, voterImage;
    public GameObject votingButtons , blocker;

    public GameObject votersGridContainer , voterObject;


    public void shout()
    {
        Debug.Log("voting succ");
    }

    

    public void setStatus(PlayerManager _player)
    {
        id = _player.id;

        color.GetComponent<Image>().color = _player.gameObject.GetComponent<bodyManager>().mainColor;
        name.text = _player.username;
        if (GameManager.players[client.instance.myId].isImposter && _player.isImposter)
        {
            name.color = Color.red;
        }

        if (!GameManager.players[_player.id].GetComponent<PlayerManager>().stillAlive)
        {
            blocker.SetActive(true);

        }

    }



    public void enableButtons()
    {

        votingButtons.SetActive(!votingButtons.activeSelf);
        if (votingManager.instance.lastButtons != null)
        {

            if (votingManager.instance.lastButtons == votingButtons)
                return;

            votingManager.instance.lastButtons.SetActive(false);
        }

        votingManager.instance.lastButtons = votingButtons;
    }


    public void choosen()
    {
        votingManager.instance.lastButtons.SetActive(false);
       ClientSend.voteOn(id);
        votingManager.instance.stopVotingAble();

    }
    



    public void cancel()
    {
        votingManager.instance.lastButtons.SetActive(false);
        votingManager.instance.lastButtons = null;
    }
}
