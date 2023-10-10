using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lore : MonoBehaviour
{
    public static bool GamePaused = false;
    GameObject loreUi;
    public bool update = false;
    int currentPage = 0;
    public List<Sprite> allpages = new List<Sprite>();
    Image pic; GameObject page;
    Animator enem; GameObject enemAnims;
    Image lorenotif; GameObject lorestuff;
    public Sprite nonotifIma;
    public Sprite notifIma;
    //public int max = 8; 

    public static lore loe;

    private void Start()
    {
        


    }

    private void Awake()
    {
        loreUi = GameObject.Find("LoreUI");
        loreUi.SetActive(false);

        

    }

    void Update()
    {
        


        
        

    }

    public void LoreResume()
    {
        loreUi.SetActive(false);
        Time.timeScale = 1f;

        GamePaused = false;

       


    }

    public void LorePause()
    {
        loreUi.SetActive(true);
        Time.timeScale = 0f;
        page = GameObject.Find("LoreBookPics");
        pic = page.GetComponent<Image>();
        pic.sprite = allpages[currentPage];

       

        if (update == false)
        {
            lorestuff = GameObject.Find("Lorebook");
            lorenotif = lorestuff.GetComponent<Image>();
            lorenotif.sprite = nonotifIma;
        }
        else
        {
            lorestuff = GameObject.Find("Lorebook");
            lorenotif = lorestuff.GetComponent<Image>();
            lorenotif.sprite = notifIma;
        }

        if (update == true)
        {
            update = false;
        }

        Lstate();


        GamePaused = true;


    }

    public void MoveLeft()
    {
        /*if (PlayerController.player.lore1 == true)
        {
            if (currentPage > 1)
            {
                currentPage--;
                page = GameObject.Find("LoreBookPics");
                pic = page.GetComponent<Image>();
                pic.sprite = allpages[currentPage];
            } 
        }*/ 
         if (currentPage > 0)
        {
            currentPage--;
            page = GameObject.Find("LoreBookPics");
            pic = page.GetComponent<Image>();
            pic.sprite = allpages[currentPage];

            Lstate();
        }

        


    }

    public void MoveRight()
    {
        /*(if (PlayerController.player.lore1 == true)
        {
            max = 2; 
        }
        else if (PlayerController.player.lore2 == true)
        {
            max = 5; 
        }
        else if (PlayerController.player.lore3 == true)
        {
            max = 7; 
        }
        else if (PlayerController.player.lore4 == true)
        {
            max = allpages.Count - 1; 
        }*/

        if (currentPage < allpages.Count - 1)
        {
            currentPage++;
            page = GameObject.Find("LoreBookPics");
            pic = page.GetComponent<Image>();
            pic.sprite = allpages[currentPage];


            Lstate();
        }





    }

    private void Lstate()
    {
        enemAnims = GameObject.Find("EnemyAnims");
        enem = enemAnims.GetComponent<Animator>();

        if (currentPage != 1 && currentPage != 4)
        {
            enem.SetInteger("lorestate", (int)currentPage);
        }
        else
        {
            enem.SetInteger("lorestate", 1);
        }





        
    }


}
