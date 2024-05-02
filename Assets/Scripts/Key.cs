using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{


    void Start()
    {
        if(!SceneParameters.key1)
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneParameters.key1 = false;
            SceneParameters.keys++;
            Destroy(gameObject);
        }
    }
}
