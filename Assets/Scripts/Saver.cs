using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class Saver
{
    public int Health;
    public int MaxHealth;
    public int BalconyExit;
    public bool CharacterDoor;
    public bool CharacterDoorKey;
    public bool CheckBomb;
    public int weaponIndex;
    public int SceneInt;
    public Saver()
    {
        Health = SceneParameters.Health;
        MaxHealth = SceneParameters.MaxHealth;
        BalconyExit = SceneParameters.BalconyExit;
        CharacterDoor = SceneParameters.CharacterDoor;
        CharacterDoorKey = SceneParameters.CharacterDoorKey;
        CheckBomb = SceneParameters.CheckBomb;
        weaponIndex = SceneParameters.weaponIndex;
        SceneInt = SceneManager.GetActiveScene().buildIndex;
    }
}
