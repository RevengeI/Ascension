using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour
{
    [SerializeField] float wait;
    [SerializeField] GameObject[] sprites;
    private int iter;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Wait", 0, wait);
        iter = sprites.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Wait()
    {
        if(iter >= 0)
        {
            sprites[iter].gameObject.SetActive(true);
            iter--;
        }
        else
        {
            Destroy(this.GetComponent<Waiter>());
        }
    }
}
