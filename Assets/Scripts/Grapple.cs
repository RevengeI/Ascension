using System.Collections;
using System;
using System.Collections.Generic;

using UnityEngine;
public class Grapple : MonoBehaviour
{
    public float speed = 10f;
    public float height = 0.18f;
    public float width = 0.18f;
    public float timeToLive = 0;
    public LayerMask Ground;
    public DistanceJoint2D joint;
    private Rigidbody2D rigid;
    private Rigidbody2D player;
    private Vector2 playerOrient;
    private Vector2 Orientation;
    private bool Grounded;
    public bool Grappled;
    private bool[] Orientations;
    


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerOrient = player.transform.localScale;
        Grounded = player.GetComponent<CharacterMoviesSideScroller>().OnGround;
        Grappled = player.GetComponent<CharacterMoviesSideScroller>().Grappled;
        Orientations = player.GetComponent<CharacterMoviesSideScroller>().Orientations;
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1f);
        rigid = gameObject.GetComponent<Rigidbody2D>(); //getting all needed components
        DeclareOrientation();
    }


    void Update()
    {
        if (!Grappled)
        {
            rigid.velocity = Orientation * speed;
        }
        if (Physics2D.OverlapBox(transform.position, new Vector2(width, height), 0, Ground))
        {
            if (!Grappled) // using the variable as a flag to execute the code just once
            {
                Attacher();
            }
        }
        if(Grappled)
        {
            PendulumMotion();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            //if(Grappled)
            //{ 
            //    player.AddForce(joint.reactionForce, ForceMode2D.Impulse); 
            //}
            Grappled = false;
            player.GetComponent<CharacterMoviesSideScroller>().Grappled = false;
            Destroy(gameObject);
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
        joint.connectedAnchor = new Vector2(0, 0);
        joint.distance = 1f;
    }

    void DeclareOrientation()
    {
            if (Orientations[0] && (player.velocity.x == 0 || !Grounded))
            {
                Orientation = new Vector2(0, 1);
            }
            else if (Orientations[2] && !Grounded)
            {
                Orientation = new Vector2(0, -1);
            }
            else
            {
                if (playerOrient.x > 0)
                {
                    Orientation = new Vector2(1, 0);
                }
                else
                {
                    Orientation = new Vector2(-1, 0);
                }
                if (Orientations[1] || (Orientations[0] && player.velocity.x != 0))
                {
                    Orientation += new Vector2(0, 1);
                }
                else if (Orientations[3] || (Orientations[2] && player.velocity.x != 0))
                {
                    Orientation += new Vector2(0, -1);
                }
            }
    }
    
    void PendulumMotion()
    {
        float angle = (Mathf.Atan2(rigid.transform.position.y - player.transform.position.y, rigid.transform.position.x - player.transform.position.x) - 1.5f )/ Mathf.PI;
        StartCoroutine(AddingForce(angle));
        if (player.velocity.x > 35)
        {
            player.velocity = new Vector2(35, player.velocity.y);
        }
        if (player.velocity.y > 35)
        {
            player.velocity = new Vector2(player.velocity.x, 35);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            joint.distance = 6f;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            joint.distance = 1f;
        }
    }

    IEnumerator AddingForce(float angle)
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < 7; i++)
            {
                if (angle > 0.5f)
                {
                    player.AddForce(new Vector2(-1 * Math.Abs(angle) * 500, 0), ForceMode2D.Force);
                }
                else
                {
                    player.AddForce(new Vector2(Math.Abs(angle) * 500, 0), ForceMode2D.Force); //40
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < 7; i++)
            {
                if (angle < -0.5f)
                {
                    player.AddForce(new Vector2(Math.Abs(angle) * 500, 0), ForceMode2D.Force);
                }
                else
                {
                    player.AddForce(new Vector2(-1 * Math.Abs(angle) * 500, 0), ForceMode2D.Force);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}