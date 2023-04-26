using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public int selector = 0;
    public GameObject[] selectables;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        selectables[selector].GetComponent<SpriteRenderer>().color = Color.red;
        if (Input.GetKeyDown(KeyCode.S))
        {
            selectables[selector].GetComponent<SpriteRenderer>().color = Color.black;
            selector++;

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectables[selector].GetComponent<SpriteRenderer>().color = Color.black;
            selector--;
        }
        if (selector < 0)
        {
            selector = selectables.Length - 1;
        }
        if (selector > selectables.Length - 1)
        {
            selector = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (selector)
            {
                case 0:
                    Time.timeScale = 1f;
                    GameObject.Find("PauseCaller").GetComponent<CallPauseMenu>().paused = false;
                    break;
                case 1:
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(2);
                    break;
            }
        }
    }
}
