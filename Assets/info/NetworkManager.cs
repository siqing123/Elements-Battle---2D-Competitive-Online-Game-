using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        if(PhotonNetwork.IsConnected)
        {
            Debug.Log("if condition");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("else condition");
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connection was successful");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("break connection");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Faile joining room");
        Debug.Log("Create new room");
        PhotonNetwork.CreateRoom(null,new RoomOptions { MaxPlayers = 8});
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join the room successfully");
        //Load game secene
        PhotonNetwork.LoadLevel(1);
    }
}
