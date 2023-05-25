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
    public LayerMask WhatsIsKey;
    public LayerMask WhatsIsBomb;
    public bool KeyDetected;
    public GameObject KeySprite;
    public GameObject BombSprite;
    public bool BombCheck = true;
    public bool[] Orientations = { false, false, false, false }; // right - down - left - up
    public SpriteRenderer[] hearts;
    public Sprite heart;

    public HealthBar HealthCharacter;
    
    private Vector2 vec2;
    private float diagonalFixed = 0.7075f;

    void Start()
    {
        KeyDetected = false;
        move = GetComponent<Rigidbody2D>();
        if (SceneParameters.BalconyExit == 2)
        {
            CharacterPosition.position = new Vector2(-29.8f, 3.43f);
        }
        if (SceneParameters.BalconyExit == 1)
        {
            CharacterPosition.position = new Vector2(-1.24f, 3.43f);
        }
        SceneParameters.BalconyExit = 0;
        KeyDetected = SceneParameters.CharacterDoorKey;
        BombCheck = SceneParameters.CheckBomb;
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


        if (Physics2D.OverlapBox(CharacterPosition.position, new Vector2(width, height), 0, WhatsIsKey))
        {
            SceneParameters.CharacterDoorKey = KeyDetected = true;
        }
        if (KeyDetected == true)
        {
            KeySprite.SetActive(false);
        }


        if (Physics2D.OverlapBox(CharacterPosition.position, new Vector2(width, height), 0, WhatsIsBomb) == true)
        {
            SceneParameters.CheckBomb = BombCheck = false;
            HealthCharacter.Health--;
        }
        if (BombCheck == false)
        {
            BombSprite.SetActive(false);
        }

        if (HealthCharacter.Health > HealthCharacter.MaxHealth)
        {
            HealthCharacter.Health = HealthCharacter.MaxHealth;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < HealthCharacter.Health)
                hearts[i].sprite = heart;
            else
                hearts[i].sprite = null;
        }
    }
}