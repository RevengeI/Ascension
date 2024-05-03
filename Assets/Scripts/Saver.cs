using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class Saver
{
    public int Health;
    public int MaxHealth;
    public int ExitNumber;
    public bool exposedCore;

    public int weaponIndex;
    public int SceneInt;
    public bool grappleGet;
    public bool shotgunGet;
    public bool clawsGet;

    public int keys;
    public bool key1;
    public bool door1;
    public Saver()
    {
        Health = SceneParameters.Health;
        MaxHealth = SceneParameters.MaxHealth;
        exposedCore = SceneParameters.exposedCore;
        ExitNumber = SceneParameters.ExitNumber;
        weaponIndex = SceneParameters.weaponIndex;
        grappleGet = SceneParameters.grappleGet;
        shotgunGet = SceneParameters.shotgunGet;
        clawsGet = SceneParameters.clawsGet;
        keys = SceneParameters.keys;
        key1 = SceneParameters.key1;
        door1 = SceneParameters.door1;

        SceneInt = SceneManager.GetActiveScene().buildIndex;
    }
}
