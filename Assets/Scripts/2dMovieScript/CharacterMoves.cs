using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMoves : MonoBehaviour
{
    public Rigidbody2D move;
    public float speedCharacter = 2f;
    public Transform CharacterPosition;
    public float width;
    public float height;
       public bool[] Orientations = { false, false, false, false }; // right - down - left - up

    
    private Vector2 vec2;
    private float diagonalFixed = 0.7075f;

    void Start()
    {
        move = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        vec2.x = Input.GetAxis("Horizontal");
        vec2.y = Input.GetAxis("Vertical");
        
        if ((vec2.x != 0) && (vec2.y != 0))
        {
            move.velocity = new Vector2(vec2.x * speedCharacter * diagonalFixed, vec2.y * speedCharacter * diagonalFixed);
        }
        else
        {
            move.velocity = new Vector2(vec2.x * speedCharacter, vec2.y * speedCharacter);
        }
        if (vec2.x > 0)
        {
            Orientations[0] = true;
            Orientations[2] = false;
            if(vec2.y == 0)
            {
                Orientations[1] = false;
                Orientations[3] = false;
            }
        }
        else if (vec2.x < 0)
        {
            Orientations[2] = true;
            Orientations[0] = false;
            if (vec2.y == 0)
            {
                Orientations[1] = false;
                Orientations[3] = false;
            }
        }
        if (vec2.y > 0)
        {
            Orientations[3] = true;
            Orientations[1] = false;
            if (vec2.x == 0)
            {
                Orientations[0] = false;
                Orientations[2] = false;
            }
        }
        else if(vec2.y < 0)
        {
            Orientations[1] = true;
            Orientations[3] = false;
            if (vec2.x == 0)
            {
                Orientations[0] = false;
                Orientations[2] = false;
            }
        }
    }
}