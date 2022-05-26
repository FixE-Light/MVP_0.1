using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("Try connection");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("TestScene");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message + returnCode);
        Debug.Log(" failed to join random game");
        CreateRoom();
    }
    public void CreateRoom()
    {
        Debug.Log(" creating room");

        System.Random rd = new System.Random();

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(rd.Next(100, 200).ToString());
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(message + returnCode);
        Debug.Log(" failed to create random game");
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinRandomRoom();
    }
}
