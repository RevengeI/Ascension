using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private GameObject player;
    public LayerMask ground;


    public float width;
    public float height;

    public Vector2 movePosition;

    private float playerX;
    private float nextX;
    private float dist;
    private float baseY;
    private float nextY;
    private float accel;
    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        accel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            flag = true;
        }
        while(flag)
        {
            playerX = player.transform.position.x;
            baseY = player.transform.position.y;
            accel++;
            nextX = playerX + accel;
            nextY = baseY + accel;
            movePosition = new Vector2(playerX + nextX, baseY + nextY);
            transform.position = movePosition;

            if (Physics2D.OverlapBox(movePosition, new Vector2(width, height), 0, ground) == true)
            {
                Destroy(gameObject);
                flag = false;
            }
        }
    }
}