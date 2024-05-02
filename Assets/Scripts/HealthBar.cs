using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] TextMeshPro health;
    [SerializeField] int indexTank;
    [SerializeField] int maxIndexTank;
    [SerializeField] GameObject[] tanks;

    void Start()
    {

    }

    void Update()
    {
        
        health.text = (SceneParameters.Health % 100).ToString("00");
        
        indexTank = (SceneParameters.Health / 100);
        maxIndexTank = (SceneParameters.MaxHealth / 100);
        TankFill();
    }

    void TankFill()
    {
        for (int i = maxIndexTank; i < 14; i++)
        {
            tanks[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0) ;
        }
        for (int i = 0; i < indexTank; i++)
        {
            tanks[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            tanks[i].gameObject.GetComponent<SpriteRenderer>().sprite = tanks[i].gameObject.GetComponent<SpritesStorage>().Storage[0];
        }
        if(indexTank > 0)
        {
            tanks[indexTank-1].gameObject.GetComponent<SpriteRenderer>().sprite = tanks[indexTank - 1].gameObject.GetComponent<SpritesStorage>().Storage[1];

        }
        for (int i = indexTank; i < maxIndexTank; i++)
        {
            tanks[i].gameObject.GetComponent<SpriteRenderer>().sprite = tanks[i].gameObject.GetComponent<SpritesStorage>().Storage[2];
        }
        
    }

}
