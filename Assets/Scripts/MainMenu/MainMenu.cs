using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public int selector = 0;
    public GameObject[] selectables;
    private bool blockMove;
    private float Axis;
    private float refVel = 0;
    [SerializeField] private GameObject bg;
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

        MoveBG(selector);

        if (Input.GetButtonDown("Submit"))
        {
            switch (selector)
            {
                case 0:
                    SceneManager.LoadSceneAsync(1);
                    break;
                case 1:
                    Saver loader;
                    BinaryFormatter binForm = new BinaryFormatter();
                    if(File.Exists(Application.persistentDataPath + @"\saves\save.dat"))
                    {
                        using (FileStream stream = new FileStream(Application.persistentDataPath + @"\saves\save.dat", FileMode.Open))
                        {
                            loader = (Saver)binForm.Deserialize(stream);
                        }
                        Loading(loader);

                        SceneManager.LoadSceneAsync(loader.SceneInt);
                        
                    }
                    break;
                case 2:
                    Application.Quit();
                    break;
            }
        }

        
    }

    public void Loading(Saver loader)
    {
        SceneParameters.Health = loader.Health;
        SceneParameters.MaxHealth = loader.MaxHealth;
        SceneParameters.exposedCore = loader.exposedCore;
        SceneParameters.ExitNumber = loader.ExitNumber;
        SceneParameters.weaponIndex = loader.weaponIndex; 
        SceneParameters.shotgunGet = loader.shotgunGet;
        SceneParameters.clawsGet = loader.clawsGet;
        SceneParameters.keys = loader.keys;
        SceneParameters.key1 = loader.key1;
        SceneParameters.door1 = loader.door1;
        SceneParameters.battery2 = loader.battery2;
        SceneParameters.battery1 = loader.battery1;
    }

    void MoveBG(int selector)
    {
        switch (selector)
        {
            case 0:
                if(bg.transform.position.y != -7)
                {
                    float newPos = Mathf.SmoothDamp(transform.position.y, -100, ref refVel, 0.6f);
                    bg.transform.position = new Vector2(0, newPos);
                }
                break;
            case 1:
                if (bg.transform.position.y != 0)
                {
                    float newPos = Mathf.SmoothDamp(transform.position.y, 0, ref refVel, 0.6f);
                    bg.transform.position = new Vector2(0, newPos);
                }
                break;
            case 2:
                if (bg.transform.position.y != 7)
                {
                    float newPos = Mathf.SmoothDamp(transform.position.y, 100, ref refVel, 0.6f);
                    bg.transform.position = new Vector2(0, newPos);
                }
                break;
        }
    }
}
