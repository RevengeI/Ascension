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
    public LayerMask Ground;
    private bool Attached = false;
    private float radius = 0.5f;
    private float degrees = 1.5f;
    private double angle;
    private float originX;
    private float originY;
    public Rigidbody2D rigidGravity;
    private int accel = 30000000;
    private int resistance = 0;
    public enum ButtonPress
    {
        Right,
        Left,
        Null
    }
    public ButtonPress Orientation;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidGravity = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        transform.position = player.transform.position;
        angle = Math.PI * degrees;
        Orientation = ButtonPress.Null;
    }
    void Update()
    {
        if (Attached)
        {
            Rotation();
        }    
        else
        { 
            transform.Translate((speed * Time.deltaTime), (speed * Time.deltaTime), 0);
            if (Physics2D.OverlapBox(transform.position, new Vector2(width, height), 0, Ground))
            {
                player.transform.position = transform.position + new Vector3(0, (-1)*radius, 0);
                originX = player.transform.position.x;
                originY = player.transform.position.y;
                rigidGravity.gravityScale = 0f;
                Attached = true;
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                Destroy(gameObject);
            }
        }
    }

    void Rotation()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            Destroy(gameObject);
            Attached = false;
            rigidGravity.gravityScale = 4f;
        }
        else
        {
            DeclareOrientation();
            if (Orientation == ButtonPress.Right)
            {
                accel += 2000;
            }
            if (Orientation == ButtonPress.Left)
            {
                accel -= 2000;
            }
            if (Orientation == ButtonPress.Null)
            {
                while (accel != 30000000)
                {
                    if (accel > 30000000)
                    {
                        accel -= 1;
                    }
                    else
                    {
                        accel += 1;
                    }    
                }
            }
            Debug.Log(accel);
            degrees = 0.00000005f * (float)accel;
            angle = Math.PI * degrees; //default 1.5f
            player.transform.position = new Vector2(originX + (float)Math.Cos(angle) * radius, originY + (float)Math.Sin(angle) * radius);
        }
    }

    void DeclareOrientation()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            Orientation = ButtonPress.Null;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Orientation = ButtonPress.Right;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Orientation = ButtonPress.Left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            radius = 3f;
        }
    }
}


/*
 X := originX + cos(angle)*radius;
 Y := originY + sin(angle)*radius;
 */