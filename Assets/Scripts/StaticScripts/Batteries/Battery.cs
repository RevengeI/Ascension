using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Battery : UpgradeTemplate
{
    

    protected override void Upgrade()
    {
        SceneParameters.MaxHealth += 100;
        SceneParameters.Health = SceneParameters.MaxHealth;
        SceneParameters.battery1 = false;

    }

    protected override void DestroyIfCollected()
    {
        if (!SceneParameters.battery1)
        {
            Destroy(gameObject);
        }
        else
        {
            meCanvas = gameObject.transform.GetChild(0).gameObject.GetComponent<Canvas>();
            meCanvas.worldCamera = Camera.main;
        }
    }
}
