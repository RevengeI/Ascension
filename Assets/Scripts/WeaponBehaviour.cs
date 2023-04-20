using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponBehaviour : MonoBehaviour
{
    public bool[] weapons;
    public GameObject GrappleSS;
    public GameObject ShotgunSS;
    public GameObject GrappleTD;
    public GameObject ShotgunTD;
    [SerializeField] private int index = 0;
    public SpriteRenderer Weapon;
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
        if (GameObject.FindWithTag("RoomType").GetComponent<RoomTypeChecker>().RoomType == true)
        {
            if (index == 0)
            {
                Instantiate(GrappleSS, transform.position, Quaternion.identity);
            }
            if (index == 1)
            {
                for (int i = 0; i < 7; i++)
                {
                    Instantiate(ShotgunSS, transform.position, Quaternion.identity);
                }
            }
        }
        else
        {
            if (index == 0)
            {
                Instantiate(GrappleTD, transform.position, Quaternion.identity);
            }
            if (index == 1)
            {
                for (int i = 0; i < 7; i++)
                {
                    Instantiate(ShotgunTD, transform.position, Quaternion.identity);
                }
            }
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
