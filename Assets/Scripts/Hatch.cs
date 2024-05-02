using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hatch : MonoBehaviour
{
    Animator animator;
    int EntryNumber;
    // Start is called before the first frame update
    void Start()
    {
        EntryNumber = transform.GetChild(1).GetComponent<EntryHelper>().entryNumber; ;
        animator = GetComponent<Animator>();
        Closing();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            animator.SetBool("Open", true);
        }
    }

    void Closing()
    {
        
            if(SceneParameters.ExitNumber == EntryNumber)
            {
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                animator.SetTrigger("Closing");
            }

    }

    void StopCollision()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void AddCollision()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
