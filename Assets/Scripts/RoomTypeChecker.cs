using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTypeChecker : MonoBehaviour
{
    public bool RoomType; // false = TopDown; true == SideScroll
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] exits;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        exits = GameObject.FindGameObjectsWithTag("Transitions");
        foreach (GameObject exit in exits)
        {
            if (exit.transform.GetChild(1).GetComponent<EntryHelper>().entryNumber == SceneParameters.ExitNumber)
            {
                player.transform.position = exit.transform.GetChild(1).transform.position;
            }
        }
    }

}
