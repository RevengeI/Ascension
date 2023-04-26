using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPauseMenu : MonoBehaviour
{
    public GameObject Pause;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                paused = false;
                Pause.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                paused = true;
                Pause.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        if (paused == false)
        {
            Pause.SetActive(false);
        }
    }
}
