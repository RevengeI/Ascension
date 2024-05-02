using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAppearance : MonoBehaviour
{
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(Player, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
