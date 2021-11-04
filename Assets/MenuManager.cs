using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public InputField create;
    public InputField join;
    // Start is called before the first frame update
    public void CreateRoom()
    {
        RoomOptions roomOpt = new RoomOptions();
        roomOpt.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(create.text, roomOpt);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(join.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
