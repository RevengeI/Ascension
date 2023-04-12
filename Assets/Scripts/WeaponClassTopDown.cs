using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClassTopDown : MonoBehaviour
{
    public float speed = 10f;
    public float height = 0.18f;
    public float width = 0.18f;
    public float timeToLive;
    protected Rigidbody2D rigid;
    protected Rigidbody2D player;
    protected Vector2 playerOrient;
    protected Vector2 Orientation = Vector2.zero;
    protected bool[] Orientations;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        Orientations = player.GetComponent<CharacterMoves>().Orientations;
        transform.position = player.transform.position;
        rigid = gameObject.GetComponent<Rigidbody2D>(); //getting all needed components
        DeclareOrientation();
        AdditionalCall();
    }

    void DeclareOrientation()
    {
        if (Orientations[0])
        {
            Orientation += new Vector2(1, 0);
        }
        else if (Orientations[2])
        {
            Orientation += new Vector2(-1, 0);
        }
        if (Orientations[1])
        {
            Orientation += new Vector2(0, -1);
        }
        else if (Orientations[3])
        {
            Orientation += new Vector2(0, 1);
        }
    }

    public virtual IEnumerator StopLiving()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public virtual void AdditionalCall()
    {
        timeToLive = 2f;
    }

}
