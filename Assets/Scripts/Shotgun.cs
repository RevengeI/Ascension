using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponClass
{
    float RNG;

    void Update()
    {
        rigid.velocity = Orientation * speed * 4 + new Vector2 (RNG, RNG) ;
        StartCoroutine(StopLiving());
    }

    public override void AdditionalCall()
    {
        RNG = Random.Range(-5f, 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Grapple"))
        {
            Destroy(gameObject);
        }    
    }
    
}
