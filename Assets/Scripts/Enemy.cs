using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator anim;
    protected Rigidbody2D rb;
    public int health = 1;
    protected bool stunned = true;
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Attacked()
    {
        if (this.gameObject.tag != "UnKill")
        {
            health--;
            if (health <= 0)
            {
                anim.SetTrigger("Stunned");
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
                GetComponent<Collider2D>().enabled = false;
                stunned = false;
            }
            StartCoroutine(changeColor());
        }
     
       

    }

    private void Stunned()
    {

         
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        foreach (Transform child in transform)
        {
    
            Destroy(child.gameObject);
        }

        StartCoroutine(RemoveObject());
    }
    IEnumerator RemoveObject()
    {
        yield return new WaitForSeconds(3);
        StopAllCoroutines(); 

        
        Destroy(gameObject);
    }

    IEnumerator changeColor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        

    }

    public virtual void Move()
    {
        
    }

    /*rb.constraints = RigidbodyConstraints2D.FreezePosition; 

        //Wait for a bit (two seconds)
        yield return new WaitForSeconds(2);
        //And unfreeze before restoring velocities
        rb.constraints = RigidbodyConstraints2D.None;
        GetComponent<Collider2D>().enabled = true;
    */

}
