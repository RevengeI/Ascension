using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSaveDir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        System.IO.Directory.CreateDirectory(Application.persistentDataPath + @"\saves");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
