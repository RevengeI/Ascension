using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image Bar;
    public float Health;

    void Start()
    {
        Health = SceneParameters.Health;
    }

    void Update()
    {
        Bar.fillAmount = Health;
        SceneParameters.Health = Health;
    }
}
