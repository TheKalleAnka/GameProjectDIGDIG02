﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    [SerializeField]
    Transform groundCheckTransform;
    [SerializeField]
    LayerMask groundCheckLayerMask;

    [SerializeField]
    Transform rightWallCheckTransform;
    [SerializeField]
    LayerMask rightWallCheckLayerMask;

    [SerializeField]
    Transform leftWallCheckTransform;
    [SerializeField]
    LayerMask leftWallCheckLayerMask;

    //variables used in the class to set up movement in different directions
    float speed = 5;
    float jump = 13;
    float moveMultiplier = 1f;

    [SerializeField]
    float forceMovementControl;
    float contAirTime;

    [SerializeField]
    BoxCollider2D bodyBox;
    [SerializeField]
    Rigidbody2D body;

    bool grounded = false;
    bool canWallJumpRight = false;
    bool canWallJumpLeft = false;

    float timeAttachedToRightWall = 0;
    float timeAttachedToLeftWall = 0;
    float timeLeaveWallDelay = 0.3f;
	
	// Update is called once per frame
	void Update () 
    {
        //Calling methods Move and Crouch
        Move();
        Crouch();
        Jump();
        Timer();

        hasCheckedGroundedThisLoop = false;
	}

    public void Timer()
    {
        if (canWallJumpLeft)
            contAirTime = Time.time + 0.1f;
        if (Time.time >= contAirTime && forceMovementControl < 2)
        {
            forceMovementControl = forceMovementControl + 0.08f;
            contAirTime = Time.time + 0.1f;
        }
    }

    /// <summary>
    /// Handles all horizontal movement
    /// </summary>
    public void Move()
    {
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            if(CheckGrounded())
                body.velocity = new Vector2(speed * moveMultiplier, body.velocity.y);
            else if(Time.time > timeAttachedToLeftWall + timeLeaveWallDelay)
                body.AddForce(new Vector2(forceMovementControl, 0), ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.A))
        {
            if(CheckGrounded())
                body.velocity = new Vector2(-speed * moveMultiplier, body.velocity.y);
            else if(Time.time > timeAttachedToRightWall + timeLeaveWallDelay)
                body.AddForce(new Vector2(-forceMovementControl, 0), ForceMode2D.Impulse);
        }

        if (body.velocity.x > speed)
            body.velocity = new Vector2(speed, body.velocity.y);
        else if (body.velocity.x < -speed)
            body.velocity = new Vector2(-speed, body.velocity.y);

        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            body.velocity = new Vector2(body.velocity.x * 0.92f, body.velocity.y);
    }

    /// <summary>
    /// Check if the player attaches to a wall
    /// </summary>
    /// <param name="col"></param>
    public void OnCollisionEnter2D(Collision2D col)
    {
        /*
        if (col.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            forceMovementControl = 2f;
        }
        if (col.gameObject.CompareTag("RightWall") && !grounded)
        {
            canWallJumpLeft = true;
            timeAttachedToRightWall = Time.time;
        }
        if (col.gameObject.CompareTag("LeftWall") && !grounded)
        {
            canWallJumpRight = true;
            timeAttachedToLeftWall = Time.time;
        }*/

        if (Physics2D.Linecast(transform.position, leftWallCheckTransform.position, leftWallCheckLayerMask).transform != null)
            timeAttachedToLeftWall = Time.time;
        else if (Physics2D.Linecast(transform.position, rightWallCheckTransform.position, rightWallCheckLayerMask).transform != null)
            timeAttachedToRightWall = Time.time;
    }

    /// <summary>
    /// NOT CURRENTLY USED
    /// </summary>
    /// <param name="col"></param>
    public void OnCollisionExit2D(Collision2D col)
    {/*
        if (col.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
        if (col.gameObject.CompareTag("RightWall"))
        {
            canWallJumpLeft = false;
        }
        if (col.gameObject.CompareTag("LeftWall"))
        {
            canWallJumpRight = false;
        }*/
    }


    /// <summary>
    /// Handles crouching
    /// </summary>
    public void Crouch()
    {
        //Check whether the player should start crouching
        if (Input.GetKeyDown(KeyCode.S) && CheckGrounded())
        {
            bodyBox.size = new Vector2(bodyBox.size.x, 0.5f);
            bodyBox.offset = new Vector2(bodyBox.offset.x, -0.24f);

            moveMultiplier = 0.5f;
        }
        //Check whether the player should stop crouching
        else if (Input.GetKeyUp(KeyCode.S))
        {
            bodyBox.size = new Vector2(bodyBox.size.x, 1);
            bodyBox.offset = new Vector2(bodyBox.offset.x, 0);

            moveMultiplier = 1;
        }
    }

    /// <summary>
    /// Handles all kind of jumps
    /// </summary>
    public void Jump()
    {
        //Normal jump
        if (Input.GetKeyDown(KeyCode.W) && CheckGrounded())
            body.velocity = new Vector2(body.velocity.x, jump);

        //Wall jump left
        if (!CheckGrounded() && Physics2D.Linecast(transform.position, rightWallCheckTransform.position, rightWallCheckLayerMask).transform != null && Input.GetKeyDown(KeyCode.W))
        {
            body.velocity = new Vector2(-speed * 1f, jump);
            forceMovementControl = 0.1f;
        }
        //Wall jump right
        else if (!CheckGrounded() && Physics2D.Linecast(transform.position, leftWallCheckTransform.position, leftWallCheckLayerMask).transform != null && Input.GetKeyDown(KeyCode.W))
        {
            body.velocity = new Vector2(speed * 1f, jump);
            forceMovementControl = 0.1f;
        }
    }

    bool isGrounded = false;
    bool hasCheckedGroundedThisLoop = false;

    /// <summary>
    /// Checks if the character is grounded.
    /// The bool isGrounded should not be checked
    /// since it may not be up to date.
    /// Calling this method only checks whether the
    /// character is grounded at a minimum of one time 
    /// per update.
    /// If it has been cecked this turn it will simply
    /// return isGrounded and by doing so does not 
    /// tax the system any extra.
    /// </summary>
    /// <returns>A bool which indicates whether the character is gorunded or not</returns>
    bool CheckGrounded()
    {
        if (!hasCheckedGroundedThisLoop)
        {
            if (Physics2D.Linecast(transform.position, groundCheckTransform.position, groundCheckLayerMask).transform != null)
                isGrounded = true;
            else
                isGrounded = false;

            hasCheckedGroundedThisLoop = true;
        }

        return isGrounded;
    }
}
