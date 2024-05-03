using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposedCore : MonoBehaviour
{
    [SerializeField] GameObject energyHUD;
    [SerializeField] GameObject heartHUD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if(SceneParameters.exposedCore == false)
        {
            energyHUD.SetActive(true);
            heartHUD.SetActive(false);
        }
        else if(SceneParameters.exposedCore == true)
        {
            energyHUD.SetActive(false);
            heartHUD.SetActive(true);
        }
    }
}
