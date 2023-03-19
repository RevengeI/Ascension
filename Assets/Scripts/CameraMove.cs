using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform CharacterTransform;
    public float dist = -5;

    private Vector3 Distance;

    void Update()
    {
        Distance = new Vector3()
        {
            x = CharacterTransform.position.x,
            y = CharacterTransform.position.y,
            z = dist,
        };

        this.transform.position = Distance;
    }

}