using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStation : MonoBehaviour
{
    public bool used;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!used) {
            if (other.gameObject.CompareTag("Player"))
            {
                if (Mathf.Abs(other.gameObject.GetComponent<Rigidbody2D>().velocity.x) < 0.5 &&
                    Mathf.Abs(other.gameObject.GetComponent<Rigidbody2D>().velocity.y) < 0.5
                    )
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    used = true;
                }

            }
        }

        
    }
}
