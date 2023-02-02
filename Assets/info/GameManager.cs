using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviourPun
{
   
    public GameObject player1;
    public GameObject player2;
    private float temp;
    bool doOnce = false;
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(this.player1.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            doOnce = true;
        }
    }

     void Update()
     {
        if (doOnce)
        {
            if (GameObject.Find("Player01(Clone)"))
            {
                PhotonNetwork.Instantiate(this.player2.name, Vector3.zero, Quaternion.identity);
                doOnce = false;
                return;
            }

            if (GameObject.Find("Player02(Clone)"))
            {
                PhotonNetwork.Instantiate(this.player1.name, Vector3.zero, Quaternion.identity);
                doOnce = false;
                return;
            }
        }
    }
}
