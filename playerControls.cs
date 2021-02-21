using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool onGround;
    bool jumpKeyPressed;

    float moveSpeed = 20f;
    float dirX;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Im alive!");
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

        //Check that our player is on ground
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //Give dirX the x-axels acceleration times moveSpeed
        dirX = Input.acceleration.x * moveSpeed;
        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -17.5f, 7.5f), transform.position.y);

        //We Check that Fire1 is being pushed and that our palyer is touching ground before we allow jump
        if (Input.GetButtonDown("Fire1") && onGround == true)
        {
            jumpKeyPressed = true;
            Debug.Log("Jump True");
        }
        else jumpKeyPressed = false;
        
       

       
    }

    private void FixedUpdate()
    {
        //Call Move
        Move();
        //Call Jump
        Jump();
    }

    //Move the player along the phones gyroscope
    private void Move()
    {
        rb.velocity = new Vector2(dirX, 0f);
    }

    //Jump does not work correctly because of the fixedUpdate but I don't know how to fix it and I'm running out of time (Note: Fix some other time, it only jumps sometimes if pressed more than once)
    private void Jump()
    {
        //Check that Jump key is being pressed
        if (jumpKeyPressed == true)
        {
            //Add force to the physicObject (Player)
            rb.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
            //Set false so that we wont jump constantly 
            jumpKeyPressed = false;
            Debug.Log("Jump False");
        }

    }

}
