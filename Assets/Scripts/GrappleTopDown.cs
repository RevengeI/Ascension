using System.Collections;
using System;
using System.Collections.Generic;

using UnityEngine;
public class GrappleTopDown : WeaponClassTopDown
{
    public bool Pulled = false;
    public GameObject enemy;
    public LineRenderer line;
    public override void AdditionalCall()
    {
        timeToLive = 1.2f;
    }

    public override IEnumerator StopLiving()
    {
        yield return new WaitForSeconds(timeToLive);
        if(!Pulled)
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        line.SetPosition(0, new Vector3(player.transform.position.x, player.transform.position.y, 0));
        line.SetPosition(1, new Vector3(transform.position.x, transform.position.y, 0));
        if (!Pulled)
        {
            StartCoroutine(StopLiving());
            rigid.velocity = Orientation * speed;
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Destroy(gameObject);
        }
        if(Pulled)
        {
            PullPunch(enemy);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Destroy(gameObject);
            }
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            {
                enemy = other.gameObject;
                Pulled = true;
                
            }
        
    }

    void PullPunch(GameObject other)
    {
        Pulled = true;
        other.GetComponent<DefaultEnemyTopDown>().stopCollisions = true;
        transform.position = other.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
        other.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime);
        if(Vector2.Distance(player.transform.position, other.transform.position) < 2)
        {
            other.GetComponent<DefaultEnemyTopDown>().launcher = true;
            other.GetComponent<DefaultEnemyTopDown>().health -= 20;
            other.GetComponent<Rigidbody2D>().AddForce(Orientation * 15, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }
}