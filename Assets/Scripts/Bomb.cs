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
        
        if (other.tag == "Player")
        {
            BombSprite.SetActive(false);
            SceneParameters.Health -= 3;
        }
    }
}
