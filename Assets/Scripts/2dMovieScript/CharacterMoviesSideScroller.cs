using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoviesSideScroller : MonoBehaviour
{
    public Rigidbody2D move;
    public float speedCharacter = 2f;
    public float jumpForce;
    public bool OnGround;
    public Transform GroundCheck;
    public float height = 0.18f;
    public float width = 1f;
    public LayerMask Ground;

    private Vector2 vec2;

    void Start()
    {
       move = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        jumpForce = 10;
        vec2.x = Input.GetAxis("Horizontal");
        move.velocity = new Vector2(vec2.x * speedCharacter, move.velocity.y);
        if (OnGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                move.velocity = new Vector2(move.velocity.x, jumpForce);
            }
            
        }
        ChekingGround();
    
    }

    void ChekingGround()
    {
        OnGround = Physics2D.OverlapBox(GroundCheck.position, new Vector2(width, height), 0, Ground);
    }
}
