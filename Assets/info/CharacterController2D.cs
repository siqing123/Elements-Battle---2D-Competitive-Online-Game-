using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterController2D : MonoBehaviourPun
{
    private Animator animator;

    [SerializeField] private LayerMask m_WhatIsGround;                          
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField]
    private int owerId;
    [SerializeField]
    private float m_JumpForce = 400f;

    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = 0.05f;	// How much to smooth out the movement

    [SerializeField]
    private float speed;

    const float k_GroundedRadius = .2f;

    private bool m_Grounded = true;

    private Vector3 mScale;
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mScale = transform.localScale;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        check();

        if (m_Rigidbody2D.velocity.x == 0)
        {
            animator.SetFloat("speed", 0);
        }

       if (Input.GetKey(KeyCode.D))
       {
           Move(speed,false);
           animator.SetFloat("speed", Mathf.Abs(m_Rigidbody2D.velocity.x));
       }
       if (Input.GetKey(KeyCode.A))
       {
           Move(-speed,false);
            animator.SetFloat("speed", Mathf.Abs(m_Rigidbody2D.velocity.x));
       }
        if (Input.GetKey(KeyCode.Space))
        {
            Move(-0, true);
            animator.SetBool("isJumping", true);
        }
    }

    private void Move(float move,bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 5.0f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }

        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    private void Flip()
    {

        m_FacingRight = !m_FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        mScale = theScale;
    }

    public void setId(int num)
    {
        owerId = num;
    }

    private void check()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                animator.SetBool("isJumping", false);
            }
        }
    }

   
}

