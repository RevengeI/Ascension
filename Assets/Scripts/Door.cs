using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] HelpSprite;
    public LayerMask WhatsIsCharacter;
    public Transform Pos;
    public float width;
    public float hight;
    public Transform Pivot;

    private bool CharacterDetected;
    private bool isOpen;
    [SerializeField] Animator animator;

    void Start()
    {
        if (isOpen == true)
        {
            isOpen = !isOpen;
            isOpened();
        }
    }

    void Update()
    {
        CharacterDetected = Physics2D.OverlapBox(Pos.position, new Vector2(width, hight), 0, WhatsIsCharacter);

        if (CharacterDetected == true)
        {
            if (SceneParameters.keys > 0 || SceneParameters.door1)
            {
                HelpSprite[0].SetActive(true);

                if (Input.GetButtonDown("Door"))
                {
                    if(!SceneParameters.door1)
                    {
                        SceneParameters.door1 = true;
                        SceneParameters.keys--;
                    }
                    isOpened();
                }
            }

            else
            {
                HelpSprite[1].SetActive(true);
            }
        }
        else
        {
            HelpSprite[0].SetActive(false);
            HelpSprite[1].SetActive(false);
        }
    }


    public void isOpened()
    {
        if (!isOpen)
        {
            Pivot.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), 1);
        }
        else
        {
            Pivot.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 1);
        }
        isOpen = !isOpen;
    }
}
