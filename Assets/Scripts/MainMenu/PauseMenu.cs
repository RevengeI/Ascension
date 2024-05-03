using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public int selector = 0;
    public GameObject[] selectables;
    private bool blockMove;
    private float Axis;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        selectables[selector].GetComponent<SpriteRenderer>().color = Color.red;
        Axis = Input.GetAxisRaw("Vertical");
        if (Axis != 0 && !blockMove)
        {
            blockMove = true;
            selectables[selector].GetComponent<SpriteRenderer>().color = Color.black;
            selector -= (int)Axis;
        }
        if (Axis == 0)
        {
            blockMove = false;
        }
        if (selector < 0)
        {
            selector = selectables.Length - 1;
        }
        else if (selector > selectables.Length - 1)
        {
            selector = 0;
        }

        if (Input.GetButtonDown("Start") || Input.GetButtonDown("Submit"))
        {
            switch (selector)
            {
                case 0: //Resume
                    Time.timeScale = 1f;
                    GameObject.Find("PauseCaller").GetComponent<CallPauseMenu>().paused = false;
                    break;
                case 1: //Load last Checkpoint
                    Saver loader;
                    BinaryFormatter binForm = new BinaryFormatter();
                    if (File.Exists(Application.persistentDataPath + @"\saves\save.dat"))
                    {
                        using (FileStream stream = new FileStream(Application.persistentDataPath + @"\saves\save.dat", FileMode.Open))
                        {
                            loader = (Saver)binForm.Deserialize(stream);
                        }
                        MainMenu menuCall = new MainMenu();
                        menuCall.Loading(loader);

                        SceneManager.LoadSceneAsync(loader.SceneInt);

                    }
                    break;
                case 2: //Main
                    Time.timeScale = 1f;
                    GameObject Converter = GameObject.FindGameObjectWithTag("Converter");
                    Destroy(Converter);
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }


}
