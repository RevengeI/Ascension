using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionBlock : MonoBehaviour
{
    public int sceneMovie;
    public int doorNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneParameters.ExitNumber = doorNumber;
            SceneManager.LoadScene(sceneMovie);
        }
        
    }
}
