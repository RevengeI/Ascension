using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputWindow : MonoBehaviour
{
    [SerializeField] private GameObject HelpSprite;
    [SerializeField] private int SceneMovie;


    public bool isCharacter = false;
    [SerializeField] private int DoorNumber;

    public void Start()
    {
        HelpSprite.SetActive(false);
    }

    public void Update()
    {
        if (isCharacter == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneParameters.BalconyExit = DoorNumber;
                SceneManager.LoadScene(SceneMovie);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isCharacter = true;
            HelpSprite.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)    
    {
        HelpSprite.SetActive(false);
        isCharacter = false;
    }
}
