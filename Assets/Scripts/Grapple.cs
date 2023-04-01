using System.Collections;

using System.Collections.Generic;

using UnityEngine;
public class Grapple : MonoBehaviour
{
    public GameObject player;
    public float speed = 10;
    public float height = 0.18f;
    public float width = 1f;
    public LayerMask Ground;
    public bool NoDestroy = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        
    }
    void Update()
    {
        transform.Translate((speed * Time.deltaTime), (speed * Time.deltaTime), 0);
        if (Physics2D.OverlapBox(transform.position, new Vector2(width, height), 0, Ground))
        {
            player.transform.position = transform.position;
            Destroy(gameObject);
        }
        Destroy(gameObject, 0.5f);
    }
}