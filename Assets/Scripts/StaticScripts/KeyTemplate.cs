using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTemplate : MonoBehaviour
{

    void Start()
    {

        DestroyIfCollected();
    }
    
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            KeyChain();
            Destroy(gameObject);
        }
    }

    protected virtual void DestroyIfCollected()
    {

    }

    protected virtual void KeyChain()
    {

    }
}
