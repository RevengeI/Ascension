using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool failsafe = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && failsafe)
        {
            if(SceneParameters.invincible == true)
            {
                return;
            }
            collision.gameObject.GetComponent<CharacterMoviesSideScroller>().damaged = true;
            failsafe = false;
            SceneParameters.Health -= damage;
            StartCoroutine("DamageDeal", SceneParameters.Health);
        }    
    }


    IEnumerator DamageDeal()
    {

        if(SceneParameters.exposedCore)
        {
            yield return new WaitForSeconds(5f);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
        }
        failsafe = true;

    }
}
