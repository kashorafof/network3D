using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
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

    public GameObject startMenu;
    public InputField usernameField;

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        client.instance.ConnectToServer();
    }


    public void enableButtons()
    {
        GameObject childButtons = gameObject.transform.Find("Voting buttons").gameObject;

        childButtons.SetActive(!childButtons.activeSelf);
        if(votingManager.instance.lastButtons != null)
            votingManager.instance.lastButtons.SetActive(false);
        votingManager.instance.lastButtons = childButtons;
    }
}
