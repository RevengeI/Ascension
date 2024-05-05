using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : KeyTemplate
{

    protected override void DestroyIfCollected()
    {
        if (!SceneParameters.key1)
        {
            Destroy(gameObject);
        }
    }

    protected override void KeyChain()
    {
        SceneParameters.key1 = false;
        SceneParameters.keys++;
    }
}
