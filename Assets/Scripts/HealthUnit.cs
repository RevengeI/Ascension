using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUnit : MonoBehaviour
{
    public HealthBar HealthCharacter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SceneParameters.Health < SceneParameters.MaxHealth)
            {
                HealthCharacter.Health++;
                Destroy(gameObject);
            }
        }
    }
}
