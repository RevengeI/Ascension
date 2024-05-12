using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Converter : MonoBehaviour
{
    private GameObject roomType;

    [SerializeField] GameObject playerTopDown;
    [SerializeField] GameObject playerSideScroll;
    void Start()
    {
        
        DontDestroyOnLoad(this);
        TypeSwitch();
    }



    private void OnEnable()
    {
        SceneManager.sceneLoaded += loadLevel;
    }
    void Update()
    {
        SceneManager.activeSceneChanged += activeSceneChange;
        
    }

    void activeSceneChange(Scene arg0, Scene arg1)
    {
        TypeSwitch();
    }

    void loadLevel(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Additive)
        {
            return;
        }
        else
        {
            TypeSwitch();
        }
    }

    void TypeSwitch()
    {
        roomType = GameObject.FindGameObjectWithTag("RoomType");
        if (roomType.GetComponent<RoomTypeChecker>().RoomType) // true - Topdown; false - SideScroll
        {
            playerTopDown.SetActive(false);
            playerSideScroll.SetActive(true);
        }
        else
        {
            playerTopDown.SetActive(true);
            playerSideScroll.SetActive(false);
        }
    }
}
