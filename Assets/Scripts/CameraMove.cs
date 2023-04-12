using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform CharacterTransform;
    public float dist = -1;
    public float offsetX;
    public float offsetY;

    private Vector3 Distance;

    void Update()
    {
        Distance = new Vector3()
        {
            x = CharacterTransform.position.x + offsetX,
            y = CharacterTransform.position.y + offsetY,
            z = dist,
        };

        this.transform.position = Distance;
    }

}