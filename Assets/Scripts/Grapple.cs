using System.Collections;
using System;
using System.Collections.Generic;

using UnityEngine;
public class Grapple : WeaponClass
{
    public DistanceJoint2D joint;
    public bool Grappled;
    public bool Pulled = false;
    public GameObject enemy;
    public LineRenderer line;
    private bool walljump = false;
    private bool walljumpcheck = false;
    private bool BlockMovement = false;
    public override void AdditionalCall()
    {
        Grappled = player.GetComponent<CharacterMoviesSideScroller>().Grappled;
        timeToLive = 0.3f;
    }

    public override IEnumerator StopLiving()
    {
        yield return new WaitForSeconds(timeToLive);
        if(!Grappled && !Pulled)
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        line.SetPosition(0, new Vector3 (player.transform.position.x, player.transform.position.y + 1f, 0));
        line.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 0));
        if (!Grappled && !Pulled)
        {
            StartCoroutine(StopLiving());
            rigid.velocity = Orientation * speed;
        }
        
        if(Grappled)
        {
            PendulumMotion();
        }

        if (Input.GetButtonUp("Shoot"))
        {
            if(walljump)
            {
                player.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                player.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                Grappled = false;
                player.GetComponent<CharacterMoviesSideScroller>().Grappled = false;
            }
            Grappled = false;
            player.GetComponent<CharacterMoviesSideScroller>().Grappled = false;
            Destroy(gameObject);
        }
        if(Pulled)
        {
            PullPunch(enemy);
        }
        if(walljumpcheck)
        {
            WallJumpCheck();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grapple"))
        {
            if (!Grappled) // using the variable as a flag to execute the code just once
            {
                transform.position = other.transform.position;
                Attacher();
            }
        }
        if (!Grappled)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                player.GetComponent<CharacterMoviesSideScroller>().Grappled = false;
                Grappled = false;
                Destroy(gameObject);
            }
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            {
                enemy = other.gameObject;
                Pulled = true;
                
            }
        }
        
    }
    void Attacher()
    {

        Grappled = true;
        player.GetComponent<CharacterMoviesSideScroller>().Grappled = true;
        rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        joint = gameObject.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
        joint.connectedBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        joint.anchor = new Vector2(0, 0);
        joint.autoConfigureDistance = false;
        joint.connectedAnchor = new Vector2(0, 1);
        joint.distance = 1f;
    }
    
    void PendulumMotion()
    {
        float posY;
        float posX;
        int sign;
        if (player.transform.position.x - transform.position.x <= 0)
        {
            posY = rigid.transform.position.y - player.transform.position.y;
            posX = rigid.transform.position.x - player.transform.position.x;
            sign = 1;
        }
        else
        {
            posY = player.transform.position.y - rigid.transform.position.y;
            posX = player.transform.position.x - rigid.transform.position.x;
            sign = -1;
        }
        float angle = Math.Abs((sign * Mathf.Atan2(posY, posX) - 1.5f )/ Mathf.PI);
        StartCoroutine(AddingForce(angle));
        if (player.velocity.x > 35)
        {
            player.velocity = new Vector2(35, player.velocity.y);
        }
        if (player.velocity.y > 35)
        {
            player.velocity = new Vector2(player.velocity.x, 35);
        }
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            joint.distance = 6f;
        }
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            joint.distance = 1f;
        }
    }

    IEnumerator AddingForce(float angle)
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && BlockMovement == false)
        {
            BlockMovement = true;
            for (int i = 0; i < 7; i++)
            {
                if (angle > 0.5f && player.transform.position.x - transform.position.x < 0)
                {
                    player.AddForce(new Vector2(-1 * angle * 500, -1 *angle * 200), ForceMode2D.Force);
                }
                else
                {
                    player.AddForce(new Vector2(angle * 500, 0), ForceMode2D.Force); //40
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && BlockMovement == false)
        {
            BlockMovement = true;
            for (int i = 0; i < 7; i++)
            {
                if (angle > 0.5f && player.transform.position.x - transform.position.x > 0)
                {
                    player.AddForce(new Vector2(angle * 500, -1 * angle * 200), ForceMode2D.Force);
                }
                else
                {
                    player.AddForce(new Vector2(-1 * angle * 500, 0), ForceMode2D.Force);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            BlockMovement = false;
        }
    }

    void PullPunch(GameObject other)
    {
        Pulled = true;
        other.GetComponent<DefaultEnemy>().stopCollisions = true;
        transform.position = other.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
        other.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
        if(Vector2.Distance(player.transform.position, other.transform.position) < 2)
        {
            other.GetComponent<DefaultEnemy>().launcher = true;
            other.GetComponent<DefaultEnemy>().health -= 20;
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(15 * player.transform.localScale.x, 15), ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Walljump")
        {
            walljumpcheck = true;
        }    
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Walljump")
        {
            walljumpcheck = false;
        }
    }

    void WallJumpCheck()
    {
        if (Math.Abs(player.transform.position.x - transform.position.x) > 0.49f && 
            Math.Abs(player.transform.position.x - transform.position.x) < 1.11f && 
            Math.Abs(player.transform.position.y - transform.position.y) > 0.30f &&
                Math.Abs(player.transform.position.y - transform.position.y) < 1.55f && joint.distance == 1f)
              {

                      player.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                      walljump = true;
              }    
        if(walljump)
        {
              if (Input.GetButtonDown("Jump"))
              {
              
                  player.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                  player.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                  player.constraints &= RigidbodyConstraints2D.FreezeRotation;
                  Grappled = false;
                player.GetComponent<CharacterMoviesSideScroller>().Grappled = false;
                player.velocity = new Vector2((player.transform.position.x - transform.position.x) * 15, 0);
                player.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
                  
                  Destroy(gameObject);
              }
              if(Input.GetAxisRaw("Vertical") == -1)
              {
                  joint.distance = 6f;
                  walljump = false;
              }
        }
    }
}
