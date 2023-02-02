using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class fireBall : MonoBehaviourPun
{
    [SerializeField]
    float damage = 10.0f;
    // private Hero hero;
    private float lifeSpan = 10.0f;
    [SerializeField]
    float projectileSpeed = 5;

    [SerializeField]
    GameObject mSkill;

    public int id;
    bool hit;

    void Start()
    {
        hit = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.IsMine == true && PhotonNetwork.IsConnected == true && hit == false)
        {
          
        }
        else
        {
            return;
        }

        if (other.gameObject.tag == ("player1"))
        {
            // other.GetComponent<Hero>().takeDamage(damage);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            PhotonNetwork.Destroy(this.gameObject);
            other.GetComponent<Hero>().takeDamage(30);
            hit = true;
        }

        if (other.gameObject.tag == "player2")
        {
          
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            PhotonNetwork.Destroy(this.gameObject);
            other.GetComponent<Hero>().takeDamage(30);
            hit = true;
        }
    }
     
    void Update()
    {
        if (lifeSpan > 0.0f)
        {
            lifeSpan -= Time.deltaTime;
        }
        else
        {
            if (photonView.IsMine == true && PhotonNetwork.IsConnected == true)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
