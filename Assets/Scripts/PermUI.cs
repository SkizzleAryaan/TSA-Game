using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PermUI : MonoBehaviour
{
    public int gems = 0;
    public int health = 4;
    public Text gemText;
    public Text healthAmount;
    public bool dino = false;
    public bool greek = false; 

    public static PermUI perm;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        if (!perm)
        {
            perm = this;
        }
        else
        {
            Destroy(gameObject);
        }




    }

    private void Update()
    {
        /*Scene scn = SceneManager.GetActiveScene();
        if (scn.name == "TutReq")
        {
            GetComponent<Canvas>().enabled = false; 
        }*/
    }

    public void Reset()
    {
    }

    public void ResetAll()
    {
        if (dino == true)
        {
            health = 6;
        }
        else
        {
            health = 4;
        }
        
        healthAmount.text = health.ToString();
    }
}
