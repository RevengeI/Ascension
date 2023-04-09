using System.Collections;
using System;
using System.Collections.Generic;

using UnityEngine;
public class Grapple : MonoBehaviour
{
    public GameObject player;
    public float speed = 20;
    public float height = 0.18f;
    public float width = 1f;
    public float timeToLive = 0;
    public LayerMask Ground;
    public DistanceJoint2D joint;
    private bool notAttached = true;
    private Rigidbody2D rigid;
    //private Rigidbody2D gravityPlayer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        //gravityPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (notAttached)
        {
            transform.Translate((speed * Time.deltaTime), (speed * Time.deltaTime), 0);
        }
        if (Physics2D.OverlapBox(transform.position, new Vector2(width, height), 0, Ground))
        {
            if (notAttached) // do not delete. to do: fix joint behaviour, works badly
            {
                
                notAttached = false;
                rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                joint = gameObject.AddComponent(typeof(DistanceJoint2D)) as DistanceJoint2D;
                joint.connectedBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
                joint.anchor = transform.position;
                joint.autoConfigureDistance = false;
                joint.distance = 1f;
                
            }
           /* if(!notAttached)
            {

                
                timeToLive += Time.deltaTime;
                if(timeToLive > 1f)
                {
                    // gravityPlayer.AddForce(joint.reactionForce, ForceMode2D.Impulse);
                    Destroy(gameObject);
                }
            }*/
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            //if(!notAttached)
            //{ gravityPlayer.AddForce(joint.reactionForce, ForceMode2D.Impulse); }

            Destroy(gameObject);
        }
    }

    
}