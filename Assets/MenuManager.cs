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
        Photon.Realtime.RoomOptions roomOpt = new Photon.Realtime.RoomOptions();
        roomOpt.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(create.text,roomOptions:roomOpt);
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(join.text);
    }
    public override void OnJoinedRoom()
    {
        //if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Game");
    }
}
