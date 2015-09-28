using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ControlScheme),typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    enum ClutchToWall
    {
        RIGHT,
        LEFT,
        NULL
    }

    ControlScheme cs;
    Rigidbody2D body;

    public float speed;
    bool grounded;
    Vector2 lastVelocity;

	// Use this for initialization
	void Start () {
        cs = GetComponent<ControlScheme>();
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (lastVelocity.y == 0 && lastVelocity.y == body.velocity.y)
            grounded = true;
        else
            grounded = false;

	    if(Input.GetKey(cs.left))
        {
            body.velocity = new Vector2(-speed,body.velocity.y);
        }
        if(Input.GetKey(cs.right))
        {
            body.velocity = new Vector2(speed,body.velocity.y);
        }
        if (!Input.GetKey(cs.right) && !Input.GetKey(cs.left))
        {
            body.velocity = new Vector2(0,body.velocity.y);
        }
        if(Input.GetKeyDown(cs.jump) && grounded)
        {
            body.velocity = new Vector2(body.velocity.x,3);
        }

        //Måste vara sist
        lastVelocity = body.velocity;
	}
}
