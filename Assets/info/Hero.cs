using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hero : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    public float mMaxHealth;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform muzzle;
    [SerializeField]
    float bulletSpeed;


    public float mCurrentHealth = 120;
    private float currentTime;
    private bool canHit ;

    void Awake()
    {
        mCurrentHealth = mMaxHealth;
        canHit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {         
            Shoot();
        }    
    }

    public void takeDamage(float temp)
    {
        photonView.RPC("PRC_takeDamage",RpcTarget.All,temp);
           
    }

    [PunRPC]
    void PRC_takeDamage(float temp)
    {
       // if (!photonView.IsMine)
       //     return;

        mCurrentHealth = mCurrentHealth - temp;
        if (mCurrentHealth < 0 && photonView.IsMine == true)
        {
            PhotonNetwork.Destroy(this.gameObject);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            Application.LoadLevel(0);
        }
    }

    private void Shoot()
    {
        if (transform.localScale.x > 0.0f)
        {
            GameObject mBullet = PhotonNetwork.Instantiate(bullet.name, muzzle.position, transform.rotation);
            mBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            mBullet.GetComponent<fireBall>().id = photonView.InstantiationId;
        }
        else if(transform.localScale.x < 0.0f)
        {
            GameObject mBullet = PhotonNetwork.Instantiate(bullet.name, muzzle.position, transform.rotation);
            mBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
            mBullet.GetComponent<fireBall>().id = photonView.InstantiationId;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(mCurrentHealth);
        }
        else
        {
            this.mCurrentHealth = (float)stream.ReceiveNext();
        }
    }
}
