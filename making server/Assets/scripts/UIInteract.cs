using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enableButtons()
    {
        GameObject childButtons = gameObject.transform.Find("Voting buttons").gameObject;

        childButtons.SetActive(!childButtons.activeSelf);
        if (votingManager.instance.lastButtons != null)
        {

            if (votingManager.instance.lastButtons == childButtons)
                return;

            votingManager.instance.lastButtons.SetActive(false);
        }

        votingManager.instance.lastButtons = childButtons;
    }


    public void choosen()
    {
        votingManager.instance.lastButtons.SetActive(false);
        cardManager parentCardManager = transform.parent.parent.GetComponentInParent<cardManager>();
        parentCardManager.shout();
        stopVotingAble();

    }

    void stopVotingAble() {
        votingManager.instance.votingBlocker.SetActive(true);
        votingManager.instance.skipVoting.SetActive(false);
    }

    public void cancel()
    {
        votingManager.instance.lastButtons.SetActive(false);
        votingManager.instance.lastButtons = null;
    }
}
