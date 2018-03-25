using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SwipeManager))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;
    private float jump;
    private bool slide;
    private bool grounded;
    private bool ceiled;
    [SerializeField] LayerMask WhatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private float groundRadius;
    //[SerializeField]
    //private Animation slideAnimation;
    private float currTime = 0f;
    private float animLength = 0f;
    [SerializeField]
    private Collider2D[] collider;

    private Rigidbody2D rb2d;

    [SerializeField] private Animator anim;
    private float slideTime;


    void Start () {
        rb2d = GetComponent<Rigidbody2D>();        
    }
	
	
	void Update () {
        
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, WhatIsGround);
        ceiled = Physics2D.OverlapCircle(ceilingCheck.position, groundRadius, WhatIsGround);
        
        Move();

        if ((SwipeManager.Instance.IsSwiping(SwipeDirection.Up) || Input.GetKey(KeyCode.Space)) && grounded)
            Jump();

        if ((SwipeManager.Instance.IsSwiping(SwipeDirection.Down) || Input.GetKey(KeyCode.LeftControl)) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            slide = true;
            currTime = Time.time;
            animLength = currTime + 1.5f;
            //Debug.Log("I swipe down " + animLength);
            //Debug.Log("I'm Sliding");          
            //Debug.Log(slide);
            collider[0].enabled = true;
            collider[1].enabled = false;
            collider[2].enabled = false;
        }

        if(slide && anim.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            Slide(animLength);            
        }

        anim.SetFloat("Speed", Mathf.Abs(speed));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Slide", slide);

    }

    void Move()
    {
        rb2d.velocity = new Vector2(speed * Time.fixedDeltaTime, rb2d.velocity.y);
    }

    void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce * Time.fixedDeltaTime);
    }

    void Slide(float _currTime)
    {
        //Debug.Log("Time : " + Time.time);
        if (Time.time > _currTime || !grounded)//!anim.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            slide = false;
            collider[0].enabled = false;
            collider[1].enabled = true;
            collider[2].enabled = true;
        }
        
        
    }

}
