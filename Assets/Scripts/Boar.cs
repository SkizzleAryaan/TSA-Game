using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boar : Enemy
{
    public Vector3 pointB;
    public Vector3 pointC;
    public Vector3 pointD;
    public float speed = 3.0f;
    private GameObject scndCol;

    new IEnumerator Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        var pointA = transform.position;
        scndCol = GameObject.Find("collision2");
        scndCol.SetActive(false);

        while (stunned == true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, speed));
            yield return StartCoroutine(Roar());
            yield return StartCoroutine(MoveObject(transform, pointB, pointC, speed));
            yield return StartCoroutine(Roar());
            yield return StartCoroutine(MoveObject(transform, pointC, pointD, speed));
            yield return StartCoroutine(Roar());
            transform.localScale = new Vector3(-1, 1);


            yield return StartCoroutine(MoveObject(transform, pointD, pointC, speed));
            //transform.localScale = new Vector3(1, 1);
            yield return StartCoroutine(Roar());
            //transform.localScale = new Vector3(-1, 1);
            yield return StartCoroutine(MoveObject(transform, pointC, pointB, speed));
            //transform.localScale = new Vector3(1, 1);
            yield return StartCoroutine(Roar());
            //transform.localScale = new Vector3(-1, 1);
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, speed));
            //transform.localScale = new Vector3(1, 1);
            yield return StartCoroutine(Roar());
            transform.localScale = new Vector3(1, 1);
        }
    }

    private void Update()
    {
      

        
    }

    IEnumerator MoveObject(Transform thisTransform, Vector2 startPos, Vector2 endPos, float time)
    {
        
        
        var i = 0.0f;
        var rate = speed / time;
        bool atk = anim.GetBool("Attacking");
        while (i < 1.0f && stunned == true && atk == true)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector2.Lerp(startPos, endPos, i);

            yield return null;
        }
    }

    IEnumerator Roar()
    {
           if (scndCol != null)
            {
                anim.SetBool("Attacking", false);
                if (stunned == true)
                {
                    scndCol.SetActive(true);
                }
            }
            

            yield return new WaitForSeconds(1);
            if (scndCol != null)
            {
                anim.SetBool("Attacking", true);
                scndCol.SetActive(false);
            }

        //anim.SetBool("Attacking", false);
        //yield return new WaitForSeconds(2);
        //anim.SetBool("Attacking", true);




    }
}
