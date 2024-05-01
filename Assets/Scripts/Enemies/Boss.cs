using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : DefaultEnemy
{
    
    void Start()
    {
        enemyPhysics = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!launcher)
        {
            enemyPhysics.velocity = new Vector2(orientation * speed, enemyPhysics.velocity.y);
        }

        if (orientation > 0)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
        if (orientation < 0)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (launcher)
        {
            StartCoroutine(Stun());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (stopCollisions)
        {
            GameObject[] markers = GameObject.FindGameObjectsWithTag("EnemyMarker");
            foreach (GameObject marker in markers)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), marker.GetComponent<Collider2D>());
            }
            stopCollisions = true;
        }
        if (do_once)
        {
            GameObject[] markers = GameObject.FindGameObjectsWithTag("EnemyMarker");
            foreach (GameObject marker in markers)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), marker.GetComponent<Collider2D>(), false);
            }
            do_once = false;
        }
        if (collision.gameObject.tag == "EnemyMarker")
        {
            orientation *= -1;
        }
        if (collision.gameObject.tag == "Shotgun")
        {
            health--;
        }
        if (collision.gameObject.tag == "Player")
        {
            SceneParameters.Health--;
        }
    }

    IEnumerator Stun()
    {
        yield return new WaitForSeconds(1.5f);
        launcher = false;
        do_once = true;
    }
}
