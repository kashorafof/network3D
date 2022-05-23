using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        Debug.Log("voting awake");
    }
    #endregion

    float timer;

    public GameObject lastButtons = null, skipVoting, votingBlocker , cardPrefap , cardsContainer ;
    public TextMeshProUGUI timeLeft;

    void OnEnable()
    {
        timer = 0;
        cardsDictionary = new Dictionary<int, cardManager>();
        newCard1 = Instantiate(cardPrefap);
        foreach (KeyValuePair<int, PlayerManager> player in GameManager.players)
        {
            GameObject newCard = Instantiate(cardPrefap);
            newCard.SetActive(true);
            newCard.transform.SetParent(cardsContainer.transform);
            newCard.GetComponent<cardManager>().setStatus(player.Value);
            cardsDictionary.Add(player.Key, newCard.GetComponent<cardManager>());
        }
    }

    Dictionary<int, cardManager> cardsDictionary;
    public void stopVotingAble()
    {
        votingBlocker.SetActive(true);
        skipVoting.SetActive(false);
    }

    public GameObject newCard1;

    private void Update()
    {
        timer += Time.deltaTime;
        timeLeft.text = "Time Left : " + (int) (GameManager.instance.votingTime - timer);
    }


    void OnDisable()
    {
        Debug.Log(timer);
        votingBlocker.SetActive(false);
        foreach (Transform card in cardsContainer.GetComponentInChildren<Transform>())
        {
            Destroy(card.gameObject);
        }

    }
}
