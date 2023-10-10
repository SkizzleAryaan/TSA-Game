using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenechange : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerStay2D(Collider2D collision)
    {

        GameObject play1 = GameObject.Find("Player Final");
        PlayerController playerScript = play1.GetComponent<PlayerController>();
        

        if ((collision.gameObject.tag == "Player") && playerScript.changeScreen == true )
        {
             Destroy(GameObject.FindWithTag("audioPlayer"));
            if (PermUI.perm.dino == true)
            {
                PermUI.perm.health = 6;
            }
            else
            {
                PermUI.perm.health = 4;
            } 
             SceneManager.LoadScene(sceneName);
        }
            
            
        
    }


}
