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
    public float maxSpeedWalk;
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
    public float runningSpeed = 1f;
    public bool damaged = false;
    public bool run;
    public bool[] Orientations = { false, false, false, false }; // [0] - up, [1] - up+direction, [2] - down, [3] - down+direction
    public Animator animator;
    void Start()
    {
        move = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(SceneParameters.Health > SceneParameters.MaxHealth)
        {
            SceneParameters.Health = SceneParameters.MaxHealth;
        }    
        if (Cutscened)
        {
            return;
        }
        vec2.x = Input.GetAxisRaw("Horizontal");
        if (!Sticky)
        {
            if(Mathf.Abs(move.velocity.x) < maxSpeedWalk * runningSpeed || Mathf.Sign(vec2.x) != Mathf.Sign(move.velocity.x))
            {
                move.AddForce(new Vector2(vec2.x * speedCharacter * runningSpeed, 0), ForceMode2D.Force);
            }
            if(vec2.x == 0 && move.velocity.x != 0)
            {
                move.AddForce(new Vector2(-3 * move.velocity.x, 0));
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
                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetTrigger("Jump");
                    move.isKinematic = false;
                    move.velocity = new Vector2(move.velocity.x, jumpForce);
                }

            }

        }
        animator.SetFloat("Speed", Mathf.Abs(move.velocity.x));
        animator.SetFloat("Falling", move.velocity.y);
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
            animator.SetBool("Run", run);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            run = false;
            animator.SetBool("Run", run);
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
        animator.SetBool("OnGround", OnGround);
        OrientationCheck();

        //
        //
        if(damaged)
        {
            Cutscened = true;
            move.velocity = new Vector2(0, 0);
            animator.SetTrigger("Hurt");
            if (SceneParameters.Health < 0 && SceneParameters.exposedCore == false)
            {
                SceneParameters.exposedCore = true;
            }
            else if (SceneParameters.exposedCore == true)
            {
                Die();
                damaged = false;
                return;
            }
            StartCoroutine(Invincibility());
            StartCoroutine(Knockback());
            damaged = false;
        }

    }

    void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.tag == "Slope")
        {


            if (vec2.x == 0)
            {

                move.gravityScale = 0;
            }
            /*
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
            */
            if (Input.GetButtonDown("Jump"))
            {
                //move.gravityScale = 4;
                //move.isKinematic = false;


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

    IEnumerator Invincibility()
    {
        Physics2D.IgnoreLayerCollision(3, 11, true);
        if(!SceneParameters.exposedCore)
        {
            yield return new WaitForSeconds(1.5f);
        }
        else
        {
            yield return new WaitForSeconds(5f);
        }
        Physics2D.IgnoreLayerCollision(3, 11, false);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }

    IEnumerator Knockback()
    {
        move.AddForce(new Vector2(-10 * gameObject.transform.localScale.x, 0.3f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        Cutscened = false;
        move.velocity = new Vector2(0, 0);
    }

    void Flicker()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
    }

    void Die()
    {

        Cutscened = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        move.gravityScale = 0;
        move.velocity = Vector2.zero;
        move.constraints = RigidbodyConstraints2D.FreezePosition;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        animator.SetTrigger("Die");

    }
}
