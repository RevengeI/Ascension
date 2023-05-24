using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int Health;
    public int MaxHealth;

    void Start()
    {
        Health = SceneParameters.Health;
        MaxHealth = SceneParameters.MaxHealth;
    }

    void Update()
    {
        SceneParameters.Health = Health;
        SceneParameters.MaxHealth = MaxHealth;
    }
}
