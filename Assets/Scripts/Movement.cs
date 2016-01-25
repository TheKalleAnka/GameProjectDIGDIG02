using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


    //variables used in the class to set up movement in different directions
    private float speed = 5;
    private float jump = 13;
    public float floatyMove;
    private float contAirTime;
    public BoxCollider2D bodyBox;
    public bool grounded = false;
    public Rigidbody2D body;
    public bool canWallJumpRight = false;
    public bool canWallJumpLeft = false;
	
	// Update is called once per frame
	void Update () 
    {
        //Calling methods Move and Crouch
        Move();
        Crouch();
        WallJumps();
        Timer();

	}
    public void Timer()
    {
        if (canWallJumpLeft)
        {
            contAirTime = Time.time + 1f;
        }
        if (Time.time >= contAirTime && floatyMove < 2)
        {
            floatyMove = floatyMove + 0.05f;
            contAirTime = Time.time + 0.5f;
        }
    }

    //Method used for movement
    public void Move()
    {
        if (grounded)
        {
            //move right
            if (Input.GetKey(KeyCode.D) && !canWallJumpLeft)
            {
                body.velocity = new Vector2(speed, body.velocity.y);
            }

            //move left
            if (Input.GetKey(KeyCode.A))
            {
                body.velocity = new Vector2(-speed, body.velocity.y);
            }

            //jump
            if (Input.GetKey(KeyCode.W))
            {
                body.velocity = new Vector2(body.velocity.x, jump);
            }
        }

        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                body.AddForce(new Vector2(floatyMove, 0), ForceMode2D.Impulse);
            }
            if (Input.GetKey(KeyCode.A))
            {
                body.AddForce(new Vector2(-floatyMove, 0), ForceMode2D.Impulse);
            }

            if (body.velocity.x > speed)
            {
                body.velocity = new Vector2(speed, body.velocity.y);
            }

            else if (body.velocity.x < -speed)
            {
                body.velocity = new Vector2(-speed, body.velocity.y);
            }

        }
    }

    //checking to see if the character is touching the ground
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            floatyMove = 2f;
        }
        if (col.gameObject.CompareTag("RightWall"))
        {
            canWallJumpLeft = true;
        }
        if (col.gameObject.CompareTag("LeftWall"))
        {
            canWallJumpRight = true;
        }
    }
    //checking to see when the character leaves the ground
    public void OnCollisionExit2D(Collision2D col)
    {
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
        }
    }


    //Method used for crouching
    public void Crouch()
    {
        //the actual crouching
        if (Input.GetKeyDown(KeyCode.S))
        {
            bodyBox.size = new Vector2(bodyBox.size.x, 0.5f);
            bodyBox.offset = new Vector2(bodyBox.offset.x, -0.24f);
        }
            //resets what was done in chrouching when the player lets go of the button
        else if (Input.GetKeyUp(KeyCode.S))
        {
            bodyBox.size = new Vector2(bodyBox.size.x, 1);
            bodyBox.offset = new Vector2(bodyBox.offset.x, 0);
        }
    }

    public void WallJumps()
    {
        if (canWallJumpLeft && Input.GetKey(KeyCode.W))
        {
            body.velocity = new Vector2(-speed * 2f, jump);
            floatyMove = 0.1f;
        }
        else if (canWallJumpRight && Input.GetKey(KeyCode.W))
        {
            body.velocity = new Vector2(speed * 2f, jump);
            floatyMove = 0.1f;
        }
    }

}
