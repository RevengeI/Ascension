using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public float speed = 10f;
    public float height = 0.18f;
    public float width = 0.18f;
    public float timeToLive;
    protected Rigidbody2D rigid;
    protected Rigidbody2D player;
    protected Vector2 playerOrient;
    protected Vector2 Orientation;
    protected bool Grounded;
    protected bool[] Orientations;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerOrient = player.transform.localScale;
        Grounded = player.GetComponent<CharacterMoviesSideScroller>().OnGround;
        Orientations = player.GetComponent<CharacterMoviesSideScroller>().Orientations;
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1f);
        rigid = gameObject.GetComponent<Rigidbody2D>(); //getting all needed components
        DeclareOrientation();
        AdditionalCall();
    }

    void DeclareOrientation()
    {
        if (Orientations[0] && (player.velocity.x == 0 || !Grounded))
        {
            Orientation = new Vector2(0, 1);
        }
        else if (Orientations[2] && !Grounded)
        {
            Orientation = new Vector2(0, -1);
        }
        else
        {
            if (playerOrient.x > 0)
            {
                Orientation = new Vector2(1, 0);
            }
            else
            {
                Orientation = new Vector2(-1, 0);
            }
            if (Orientations[1] || (Orientations[0] && player.velocity.x != 0))
            {
                Orientation += new Vector2(0, 1);
            }
            else if (Orientations[3] || (Orientations[2] && player.velocity.x != 0))
            {
                Orientation += new Vector2(0, -1);
            }
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
