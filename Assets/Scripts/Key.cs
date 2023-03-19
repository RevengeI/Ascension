using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public GameObject KeySprite;
    public LayerMask Player;
    public float width;
    public float hight;
    public Transform KeyPosition;

    void Start()
    {
        KeySprite.SetActive(true);
    }
    
    void Update()
    {
        if (Physics2D.OverlapBox(KeyPosition.position, new Vector2(width, hight), 0, Player) == true)
        {
            KeySprite.SetActive(false);
        }
    }
}
