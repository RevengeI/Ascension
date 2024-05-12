using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTemplate : MonoBehaviour
{
    protected Canvas meCanvas;
    private bool skip = false;
    private GameObject collision1;
    private void Start()
    {
           DestroyIfCollected();
        
    }

    private void Update()
    {
        if (skip)
        {
            if (Input.GetButtonDown("Submit"))
            {
                Time.timeScale = 1f;
                collision1.gameObject.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePosition;
                Upgrade();
                Destroy(gameObject);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(meCanvas.worldCamera == null)
        {
            meCanvas.worldCamera = Camera.main;
            meCanvas.sortingLayerName = "GetItem";
            meCanvas.sortingOrder = 2;
        }
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            collision1 = collision.gameObject;
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            meCanvas.gameObject.SetActive(true);
            StartCoroutine(Flash());
            StartCoroutine(Skip());
            meCanvas.transform.GetChild(2).gameObject.SetActive(false); 
        }
    }

    IEnumerator Flash()
    {
        yield return new WaitForSecondsRealtime(4f);
        meCanvas.transform.GetChild(2).gameObject.SetActive(true);
    }

    IEnumerator Skip()
    {
        yield return new WaitForSecondsRealtime(8f);
        skip = true;
    }

    protected virtual void Upgrade()
    {

    }

    protected virtual void DestroyIfCollected()
    {

    }
}
