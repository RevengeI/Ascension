using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponBehaviour : MonoBehaviour
{
    public bool[] weapons;
    public GameObject Grapple;
    [SerializeField] private int index = 0;
    public Image Weapon;
    Sprite GRAPPLE, SHOTGUN;
    // Start is called before the first frame update
    void Start()
    {
        index = SceneParameters.weaponIndex;
        GRAPPLE = Resources.Load<Sprite>("portable-hook-grapple");
        SHOTGUN = Resources.Load<Sprite>("portable-hook-shotgun");
        Weapon_Cycling();
    }

    void Weapon_Cycling()
    {
        if (index == 0)
        {
            Weapon.sprite = GRAPPLE;
        }
        else if (index == 1)
        {
            Weapon.sprite = SHOTGUN;
        }
    }
    void Weapon_Shoot()
    {
        if (index == 0)
        {
            Instantiate(Grapple, transform.position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        SceneParameters.weaponIndex = index;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            do
            {
                index += 1;
                if (index > weapons.Length - 1)
                {
                    index = 0;
                }
            } while (weapons[index] == false);
            Weapon_Cycling();
            
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Weapon_Shoot();
        }
    }
}
