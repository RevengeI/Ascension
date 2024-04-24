using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMoviesSideScroller : MonoBehaviour
{
    public Rigidbody2D move;
    public float speedCharacter;
    public float jumpForce;
    public bool OnGround;
    public float maxSpeed;
    public Transform CharacterPosition;
    public Transform GroundCheck;
    public float height;
    public float width;
    public LayerMask Ground;
    public Vector2 vec2;
    public bool Grappled = false;
    public bool Sticky = false;
    public bool Cutscened = false;
    public HealthBar healthBar;
    public float runningSpeed = 1f;
    public bool run;
    public bool[] Orientations = { false, false, false, false }; // [0] - up, [1] - up+direction, [2] - down, [3] - down+direction
    public SpriteRenderer[] hearts;
    public Sprite heart;
    public Animator animator;
    void Start()
    {
        move = gameObject.GetComponent<Rigidbody2D>();
        if (SceneParameters.ExitNumber == 1)
        {
            CharacterPosition.position = new Vector2(91.5f, 2f);
        }
    }

    void Update()
    {
        if (Cutscened)
        {
            return;
        }
        vec2.x = Input.GetAxis("Horizontal");
        if (!Sticky)
        {
            move.velocity = new Vector2(vec2.x * speedCharacter * runningSpeed, move.velocity.y);
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
                if (Input.GetButtonDown("Jump"))
                {
                    move.isKinematic = false;
                    move.velocity = new Vector2(move.velocity.x, jumpForce);
                }

            }

        }
        animator.SetFloat("Speed", Mathf.Abs(move.velocity.x));
        if (run)
        {
            runningSpeed = Mathf.MoveTowards(runningSpeed, 2f, 1f * Time.deltaTime);
        }
        if (!run)
        {
            runningSpeed = Mathf.MoveTowards(runningSpeed, 1f, 3f * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            run = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            run = false;
        }
        if (Grappled)
        {
            Sticky = true;
        }
        if (!Grappled && Sticky)
        {
            if (OnGround || (move.velocity.x < 10 * runningSpeed && move.velocity.x > -10 * runningSpeed))
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
                if (move.velocity.y < 0)
                {
                    move.gravityScale = 24;
                }
                else
                {
                    move.gravityScale = 4;
                }
                move.isKinematic = false;
            }
            if (Input.GetButtonDown("Jump"))
            {
                move.gravityScale = 4;
                move.isKinematic = false;


            }

        }

    }

    void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.tag == "Slope")
        {
            move.isKinematic = false;
            move.gravityScale = 4;
        }
    }

    void OrientationCheck()
    {
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            Orientations[0] = true;
        }
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            Orientations[2] = true;
        }
        if (Input.GetButtonDown("AngleUp"))
        {
            Orientations[1] = true;
        }
        if (Input.GetButtonUp("AngleUp"))
        {
            Orientations[1] = false;
        }
        if (Input.GetButtonDown("AngleDown"))
        {
            Orientations[3] = true;
        }
        if (Input.GetButtonUp("AngleDown"))
        {
            Orientations[3] = false;
        }
        if (Input.GetAxisRaw("Vertical") == 0)
        {
            Orientations[0] = false;
            Orientations[2] = false;
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
