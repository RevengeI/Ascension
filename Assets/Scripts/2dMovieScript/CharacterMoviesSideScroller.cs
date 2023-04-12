using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoviesSideScroller : MonoBehaviour
{
    public Rigidbody2D move;
    public float speedCharacter = 10f;
    public float jumpForce = 10;
    public bool OnGround;
    public Transform GroundCheck;
    public float height = 0.18f;
    public float width = 1f;
    public LayerMask Ground;
    private Vector2 vec2;
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
            move.velocity = new Vector2(vec2.x * speedCharacter, move.velocity.y);
            if (move.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (move.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (OnGround)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    move.velocity = new Vector2(move.velocity.x, jumpForce);
                }

            }
        }
        if (Grappled)
        {
            Sticky = true;
        }
        if(!Grappled && Sticky)
        {
            StartCoroutine(WaitForSticky());

        }
        CheckingGround();
        OrientationCheck();
    }

    void CheckingGround()
    {
        OnGround = Physics2D.OverlapBox(GroundCheck.position, new Vector2(width, height), 0, Ground);
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

    IEnumerator WaitForSticky()
    {
        yield return new WaitForSeconds(move.velocity.x * 0.05f);
        Sticky = false;

    }
}
