using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TRex : Enemy
{
    public Vector3 pointB;
    public Vector3 pointC;
    public Vector3 pointD;



    new IEnumerator Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        var pointA = transform.position;
        
        while (stunned == true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            yield return StartCoroutine(Roar());
            yield return StartCoroutine(MoveObject(transform, pointB, pointC, 3.0f));
            yield return StartCoroutine(Roar());
            yield return StartCoroutine(MoveObject(transform, pointC, pointD, 3.0f));
            yield return StartCoroutine(Roar());
            transform.localScale = new Vector3(-1, 1);


            yield return StartCoroutine(MoveObject(transform, pointD, pointC, 3.0f));
            transform.localScale = new Vector3(1, 1);
            yield return StartCoroutine(Roar());
            transform.localScale = new Vector3(-1, 1);
            yield return StartCoroutine(MoveObject(transform, pointC, pointB, 3.0f));
            transform.localScale = new Vector3(1, 1);
            yield return StartCoroutine(Roar());
            transform.localScale = new Vector3(-1, 1);
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
            transform.localScale = new Vector3(1, 1);
            yield return StartCoroutine(Roar());
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
           if (PermUI.perm.dino == false)
            {
                GameObject arti1 = GameObject.Find("BoneArtifact");
                arti1.SetActive(true); 
            }
           else
           {
                SceneManager.LoadScene("MainHub");
            }
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector2 startPos, Vector2 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
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
        anim.SetBool("Attacking", false); 
        yield return new WaitForSeconds(2);
        anim.SetBool("Attacking", true);


    }
}
