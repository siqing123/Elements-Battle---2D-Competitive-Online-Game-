using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cameraFollow :  MonoBehaviourPun
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    

    public Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        //{
        //    return;
        //}

        Vector3 newPos = new Vector3 (pos.position.x, pos.position.y,-10);
        transform.position = Vector3.Slerp(transform.position,newPos,FollowSpeed * Time.deltaTime);
    }
}
