using UnityEngine;
using System.Collections.Generic;

public enum CollisionSide
{
    TOP,
    BELOW,
    RIGHT,
    LEFT,
    NULL
}

[RequireComponent(typeof(ControlScheme),typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    public enum WallHang
    {
        RIGHT,
        LEFT,
        NULL
    }

    struct CurrentCollision
    {
        public CollisionSide side{get; private set;}
        public int ID { get; private set; }
        public float time { get; private set; }
        public ContactPoint2D[] contacts { get; private set; }

        public CurrentCollision(CollisionSide side, int ID, float time, ContactPoint2D[] contacts)
        {
            this.side = side;
            this.ID = ID;
            this.time = time;
            this.contacts = contacts;
        }
    }
   
    List<CurrentCollision> currentCollisions = new List<CurrentCollision>();
    bool collisionsHaveChanged = false;

    //Wallhanging
    public WallHang hangingOnWall = WallHang.NULL;

    ControlScheme cs;
    Rigidbody2D body;
    //Collider2D col2D;

    public float speed;
    public float jumpSpeed;

    public bool grounded;
    public Vector2 lastVelocity;
    public Vector2 relativeSpeedToParent;

	// Use this for initialization
	void Start () {
        cs = GetComponent<ControlScheme>();
        body = GetComponent<Rigidbody2D>();
        //col2D = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(collisionsHaveChanged)
        {
            bool touchingGround = false;
            bool touchingRight = false;
            bool touchingLeft = false;

            for(int i = 0; i < currentCollisions.Count; i++)
            {
                CurrentCollision c = currentCollisions[i];

                if (c.side == CollisionSide.BELOW)
                {
                    grounded = true;
                    touchingGround = true;
                }
                else if(c.side == CollisionSide.RIGHT)
                {
                    touchingRight = true;
                }
                else if(c.side == CollisionSide.LEFT)
                {
                    touchingLeft = true;
                }
            }

            if (!touchingGround)
            {
                grounded = false;

                if (touchingRight && Input.GetKey(cs.right))
                    hangingOnWall = WallHang.RIGHT;
                else if (touchingLeft && Input.GetKey(cs.left))
                    hangingOnWall = WallHang.LEFT;

            }
            else
                hangingOnWall = WallHang.NULL;

            //Handle anything that depends on the changes in collision above this
            collisionsHaveChanged = false;
        }


        if(hangingOnWall == WallHang.RIGHT)
        {
            if (Input.GetKey(cs.left))
            {
                body.velocity = new Vector2(-speed, body.velocity.y);
            }
            if (Input.GetKey(cs.right))
            {
                body.velocity = new Vector2(speed, body.velocity.y);
            }
            if (Input.GetKeyDown(cs.jump))
                body.velocity = new Vector2(-speed/2f,jumpSpeed);
        }
        else if(hangingOnWall == WallHang.LEFT)
        {
            if (Input.GetKey(cs.right))
            {
                body.velocity = new Vector2(speed, body.velocity.y);
            }
            if (Input.GetKey(cs.left))
            {
                body.velocity = new Vector2(-speed, body.velocity.y);
            }
        }
        else if (hangingOnWall == WallHang.NULL)
        {
            if (Input.GetKey(cs.left))
            {
                body.velocity = new Vector2(-speed, body.velocity.y);
            }
            if (Input.GetKey(cs.right))
            {
                body.velocity = new Vector2(speed, body.velocity.y);
            }
            if (!Input.GetKey(cs.right) && !Input.GetKey(cs.left))
            {
                body.velocity = new Vector2(0, body.velocity.y);
            }
            if (Input.GetKeyDown(cs.jump) && grounded)
            {
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            }
        }

        //Måste vara sist
        lastVelocity = body.velocity;
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        currentCollisions.Add(CreateCollision(col));
        collisionsHaveChanged = true;
        print("Enter");
    }

    void OnCollisionStay2D(Collision2D col)
    {/*
        //Update the currentCollisions here
        CurrentCollision c = GetCollisionByID(col.transform.GetInstanceID());

        if(c.contacts != col.contacts)
        {
            currentCollisions.Remove(c);
            currentCollisions.Add(CreateCollision(col));
            collisionsHaveChanged = true;
        }*/
    }

    void OnCollisionExit2D(Collision2D col)
    {
        CurrentCollision c = GetCollision(col.transform.GetInstanceID());
        currentCollisions.Remove(c);
        collisionsHaveChanged = true;

        print("Left");
    }

    CurrentCollision CreateCollision(Collision2D col)
    {
        CollisionSide side = CollisionSide.NULL;

        switch (col.contacts.Length)
        {
            case 1:
                if (col.contacts[0].point.y < transform.position.y)
                {
                    side = CollisionSide.BELOW;
                }
                else if (col.contacts[0].point.y > transform.position.y)
                {
                    side = CollisionSide.TOP;
                }
                else if (col.contacts[0].point.x < transform.position.x)
                {
                    side = CollisionSide.LEFT;
                }
                else if (col.contacts[0].point.x > transform.position.x)
                {
                    side = CollisionSide.RIGHT;
                }
                break;
            case 2:
                if (col.contacts[0].point.y < transform.position.y && col.contacts[1].point.y < transform.position.y)
                {
                    side = CollisionSide.BELOW;
                }
                else if (col.contacts[0].point.y > transform.position.y && col.contacts[1].point.y > transform.position.y)
                {
                    side = CollisionSide.TOP;
                }
                else if (col.contacts[0].point.x < transform.position.x && col.contacts[1].point.x < transform.position.x)
                {
                    side = CollisionSide.LEFT;
                }
                else if (col.contacts[0].point.x > transform.position.x && col.contacts[1].point.x > transform.position.x)
                {
                    side = CollisionSide.RIGHT;
                }
                break;
            default:
                side = CollisionSide.NULL;
                Debug.LogWarning("Unhandled collision case");
                break;
        }

        //Populate the currentCollisions here
        return new CurrentCollision(side, col.transform.GetInstanceID(), Time.time, col.contacts);
    }

    CurrentCollision GetCollision(int id)
    {
        for (int i = 0; i < currentCollisions.Count; i++)
        {
            if (currentCollisions[i].ID == id)
                return currentCollisions[i];
        }

        return new CurrentCollision(CollisionSide.NULL, 0, 0, null);
    }

    CurrentCollision GetCollision(CollisionSide side)
    {
        for (int i = 0; i < currentCollisions.Count; i++)
        {
            if (currentCollisions[i].side == side)
                return currentCollisions[i];
        }

        return new CurrentCollision(CollisionSide.NULL, 0, 0, null);
    }
}
