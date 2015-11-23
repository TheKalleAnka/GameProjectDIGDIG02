using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


    //variables used in the class to set up movement in different directions
    private float speed = 3;
    private float jump = 5;
    public BoxCollider2D bodyBox;
    public bool grounded = false;
    public Rigidbody2D body;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Calling methods Move and Crouch
        Move();
        Crouch();
	}

    //Method used for movement
    public void Move()
    {
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(speed, body.velocity.y);
        }
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
        }
        //jump
        if (Input.GetKey(KeyCode.W) && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jump);
        }
    }

    //checking to see if the character is touching the ground
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    //checking to see when the character leaves the ground
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            grounded = false;
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
}
