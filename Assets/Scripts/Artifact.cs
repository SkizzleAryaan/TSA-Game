using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Artifact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Scene scn = SceneManager.GetActiveScene(); 
        if (PermUI.perm.dino == false && scn.name == "DinoBoss")
        {
            PermUI.perm.dino = true;
            PermUI.perm.gems++; 
            PermUI.perm.gemText.text = PermUI.perm.gems.ToString();
        }

        if (PermUI.perm.greek == false && scn.name == "GreekBoss")
        {
            PermUI.perm.greek = true;
            PermUI.perm.gems++;
            PermUI.perm.gemText.text = PermUI.perm.gems.ToString();
        }

    }
}
