using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{
    public int selector = 0;
    public GameObject[] selectables;
    private bool blockMove;
    private float Axis;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        selectables[selector].GetComponent<SpriteRenderer>().color = Color.red;
        Axis = Input.GetAxisRaw("Horizontal");
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
                case 0: //Save
                    Save();
                    break; 
            }
            Time.timeScale = 1f;
            SceneParameters.exposedCore = false;
            gameObject.SetActive(false);
        }

    }

    void Save()
    {
        Saver save = new Saver();
        BinaryFormatter binForm = new BinaryFormatter();

        using (FileStream stream = new FileStream(Application.persistentDataPath + @"\saves\save.dat", FileMode.Create))
        {
            binForm.Serialize(stream, save);
        }
    }
}
