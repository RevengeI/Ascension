using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoviesSideScroller : MonoBehaviour
{
    public Rigidbody2D move;
    public float speedCharacter;
    public float jumpForce;
    public bool OnGround;
    public float maxSpeed;
    public Transform GroundCheck;
    public float height;
    public float width;
    public LayerMask Ground;
    public Vector2 vec2;
    public bool Grappled = false;
    public bool Sticky = false;
    public HealthBar healthBar;
    public bool[] Orientations = { false, false, false, false}; // [0] - up, [1] - up+direction, [2] - down, [3] - down+direction
    void Start()
    {
       move = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        vec2.x = Input.GetAxis("Horizontal");
        if (!Sticky)
        {
            move.AddForce(vec2 * speedCharacter, ForceMode2D.Force);
            if (move.velocity.x > 10)
            {
                move.velocity = new Vector2(10, move.velocity.y);
            }
            if (move.velocity.x < -10)
            {
                move.velocity = new Vector2(-10, move.velocity.y);
            }
            if (vec2.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (vec2.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (OnGround)
            {
                move.drag = 20;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    move.isKinematic = false;
                    move.velocity = new Vector2(move.velocity.x, jumpForce);
                }
                
            }
            if(!OnGround)
            {
                move.drag = 0;
            }    
            
        }
        if (Grappled)
        {
            Sticky = true;
            move.drag = 0;
        }
        if (!Grappled && Sticky)
        {
            if (OnGround || (move.velocity.x < 10 && move.velocity.x > -10)) 
            {
                Sticky = false;
            }
        }
        OnGround = Physics2D.OverlapBox(GroundCheck.position, new Vector2(width, height), 0, Ground);
        OrientationCheck();
    }

    void OnCollisionStay2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Slope")
        {

            
            if (vec2.x == 0)
            {
                move.velocity = new Vector2(0, 0);
                move.isKinematic = true;
            }
            if (vec2.x != 0)
            {
                if(move.velocity.y < 0)
                {
                    move.gravityScale = 24;
                }
                else
                {
                    move.gravityScale = 4;
                }
                move.isKinematic = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                move.gravityScale = 4;
                move.isKinematic = false;
                move.drag = 0;
                
                
            }
            
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        
        if (other.gameObject.tag == "Slope")
        {
            move.drag = 20;
            move.isKinematic = false;
            move.gravityScale = 4;
        }
    }
    
    void OrientationCheck()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Orientations[0] = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Orientations[0] = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Orientations[1] = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Orientations[1] = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Orientations[2] = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Orientations[2] = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Orientations[3] = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Orientations[3] = false;
        }
    }

    void FixedUpdate()
    {
        if (!GameObject.FindWithTag("Grapple"))
        {
            Grappled = false;
        }
    }
}
