using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        client.instance.tcp.SendData(_packet);
    }


    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        client.instance.udp.SendData(_packet);
    }

    #region packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }
    #endregion

    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(GameManager.players[client.instance.myId].transform.rotation);
            SendUDPData(_packet);
        }
    }

    public static void PlayerMovement(Vector2 _inputs  , bool _isCrouching , bool _isSprinting)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs);
            _packet.Write(_isCrouching);
            _packet.Write(_isSprinting);
            _packet.Write(GameManager.players[client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }
    
    public static void CameraRotation() {

        using (Packet _packet = new Packet((int)ClientPackets.cameraRotation) )
        {
            _packet.Write(GameManager.players[client.instance.myId].cam.transform.rotation);

            SendUDPData(_packet);
        }

    }


    public static void attack(GameObject attacker , GameObject secondPlayer)
    {
        using (Packet _packet = new Packet((int)ClientPackets.Attack))
        {
            _packet.Write(attacker.GetComponent<PlayerManager>().id);
            _packet.Write(secondPlayer.GetComponent<PlayerManager>().id);
            SendTCPData(_packet);
        }
    }


    public static void Report()
    {
        using (Packet _packet = new Packet((int)ClientPackets.Report))
        {
            SendTCPData(_packet);
        }
        Debug.Log("report sended");
    }


    public static void voteOn(int VotedOnId)
    {
        using(Packet _packet = new Packet((int)ClientPackets.SendVote))
        {
            _packet.Write(VotedOnId);
            SendTCPData(_packet);
        }
    }
}
