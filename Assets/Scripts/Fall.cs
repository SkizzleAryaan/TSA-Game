using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PermUI.perm.health--; 
            PermUI.perm.Reset();
            if (PermUI.perm.health <= 0)
            {
                if (PermUI.perm.dino == true)
                {
                    PermUI.perm.health = 6;
                }
                else
                {
                    PermUI.perm.health = 4;
                }
                Destroy(GameObject.FindWithTag("audioPlayer"));
                SceneManager.LoadScene("MainHub");
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            
        }
    }
}
