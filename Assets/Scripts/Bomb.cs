using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public HealthBar HealthCharacter;
    public GameObject BombSprite;

    void Start()
    {
        BombSprite.SetActive(true);
    }

    void OnTriggerEnter (Collider other)
    {
        Debug.Log("Sus");
        
        if (other.tag == "Player")
        {
            BombSprite.SetActive(false);
            HealthCharacter.Health -= 0.4f;
        }
    }
}
