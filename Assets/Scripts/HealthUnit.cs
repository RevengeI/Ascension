using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUnit : MonoBehaviour
{
    [SerializeField] int healthRefill;
    bool used = false;

    private void Update()
    {
        if(used)
        {
            if (SceneParameters.Health < SceneParameters.MaxHealth)
            {
                SceneParameters.Health += healthRefill;
                if (SceneParameters.Health > SceneParameters.MaxHealth )
                {
                    SceneParameters.Health = SceneParameters.MaxHealth;
                }
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            used = true;
        }
    }
}
