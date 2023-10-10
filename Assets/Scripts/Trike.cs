using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trike : Enemy
{
    public Vector3 pointB;
    private float prev;
    public float speed = 1.0f; 
    

    new IEnumerator Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        var pointA = transform.position;
        prev = transform.position.x;
        while (stunned == true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            transform.localScale = new Vector3(-1, 1);
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
            transform.localScale = new Vector3(1, 1);
        }
    }

    private void Update()
    {
        
        /*if (gameObject.transform.position.x > prev)
        {
            if (transform.localScale.x != -1)
            {
                transform.localScale = new Vector3(1, 1);
                prev = gameObject.transform.position.x;
            }
        }
        else
        {
            if (transform.localScale.x != 1)
            {

                transform.localScale = new Vector3(-1, 1);
                prev = gameObject.transform.position.x;
            }
        }*/

        
    }

    IEnumerator MoveObject(Transform thisTransform, Vector2 startPos, Vector2 endPos, float time)
    {
        var i = 0.0f;
        var rate = speed / time;
        while (i < 1.0f && stunned == true)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector2.Lerp(startPos, endPos, i);
            yield return null;
        }
    }


}
