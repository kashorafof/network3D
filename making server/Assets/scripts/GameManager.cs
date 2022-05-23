using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region singelton
    public static GameManager instance;
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

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject bloodSplatter , votingMenu , ReportSystem;

    public bool playAble;
    public int votingTime;

    private void Start()
    {
        playAble = true;
    }

    public void SpawnPlayer(int _id , string _username , Vector3 _position , Quaternion _rotation)
    {
        GameObject _player;
       if(_id == client.instance.myId)
       {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
            _player.GetComponent<PlayerManager>().cam.SetActive(true);
       }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
            _player.GetComponent<PlayerManager>().cam.SetActive(false);
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;

        players.Add(_id ,_player.GetComponent<PlayerManager>());
    }

    public void Report(int reporterId)
    {
        Debug.Log(players[reporterId].name + " reported and his color is" + players[reporterId].body.mainColor);
        ReportSystem.SetActive(true);
        changePlayAbility();
    }

    public void changePlayAbility()
    {
        playAble = !playAble;

        if (Cursor.lockState == CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else if (Cursor.lockState == CursorLockMode.Locked)
        {

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ClientSend.Report();

        }
    }

}
