using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraAB : Enemy
{

    public bool initial = false; 
    public int startDelay = 3;

    public GameObject vh1;
    public GameObject vh2; 


    void Update()
    {

        if (health <= 0)
        {
            if (vh1 != null)
            {
                vh1.SetActive(true);
            }
            if (vh2 != null)
            {
                vh2.SetActive(true);
            }
            
        }
        else if (initial == true)
        {
            if (vh1 != null)
            {
                vh1.SetActive(false);
            }
            if (vh2 != null)
            {
                vh2.SetActive(false);
            }
        }

      
            
        
       
    }

    // The delay coroutine
   
}
