using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cameraSetup : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
            return;

        GameObject mCamera = transform.Find("myCamera").gameObject;
        mCamera.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
