using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController player;
    //Start()
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    public bool changeScreen;
    private float playerSize = 1; 

    //Lore
    public bool lore1 = false;
    public bool lore2 = false;
    public bool lore3 = false;
    public bool lore4 = false;


    //idk 
    private Vector3 scaleChange1;
    private Vector3 scaleChange2;

    //FSM
    private enum State { idle, running, jumping, falling, hurt, attack, airattack }
    private State state = State.idle;


    //Inspector
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpForce2 = 8f;
    [SerializeField] private int cherry = 0;
    [SerializeField] private float hurtForce = .1f;



    private bool canJump;

    //Attacking
    public Transform attackPoint;
    public Transform DairPoint;
    public Transform UairPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2f;
    float nextAttackTime = 1f;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        PermUI.perm.healthAmount.text = PermUI.perm.health.ToString();
        playerSize = 1; 

        /*if (PermUI.perm.greek == true)
        {
            PermUI.perm.health = 10;

        }*/
    }

    private void Update()
    {
        Scene cScene = SceneManager.GetActiveScene();
        string sceneName = cScene.name;

        //scaleChange1 = new Vector3(playerSize, playerSize, 0);
        //this.transform.localScale = scaleChange1;

        if (Input.GetKey(KeyCode.E))
        {
            changeScreen = true; 
        }
        else
        {
            changeScreen = false;
        }

        if (state != State.hurt)
        {
            Movement();

            if (Time.time >= nextAttackTime)
            {
                if ((Input.GetMouseButtonDown(0)) || (Input.GetKeyDown(KeyCode.Return)) && sceneName != "MainHub")
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;

                }
            }

        }
        
        if (sceneName == "MainHub")
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Unequip"), 1f);
            
            if (PermUI.perm.dino == true)
            {
                PermUI.perm.health = 6;
            }
            else
            {
                PermUI.perm.health = 4;
            }
            PermUI.perm.healthAmount.text = PermUI.perm.health.ToString();
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Unequip"), 0f);
        }

        //Lore
        /*if (sceneName == "Tutorial")
        {
            lore1 = true;
            //lore.loe.update = true; 
        }
        if (sceneName == "DinoLvl1")
        {
            lore2 = true;
            //lore.loe.update = true;
        }
        if (sceneName == "DinoLvl2")
        {
            lore3 = true;
            //lore.loe.update = true;
        }
        if (sceneName == "DinoBoss")
        {
            lore4 = true;
            //lore.loe.update = true;
        }*/






        VelocityState();
        anim.SetInteger("state", (int)state); //Set anim based on Inum state
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            PermUI.perm.gems++;
            PermUI.perm.gemText.text = PermUI.perm.gems.ToString();
        }

        if (collision.tag == "Powerup")
        {
            Destroy(collision.gameObject);
            cherry++;
            canJump = true;
            //Could also do speed: speed = 20f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPower()); //Timer method
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.layer == 9)
        {
            state = State.hurt;
            StartCoroutine(colorChanger());
            if (collision.gameObject.tag == "Boar")
            {

                StartCoroutine(boarAttack());
            }
            HandleHealth();

            

            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                Invoke("Zero", .5f);
            }
            else
            {
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                Invoke("Zero", .5f);
            }
        }
    }

    private void HandleHealth()
    {
        PermUI.perm.health--;
        PermUI.perm.healthAmount.text = PermUI.perm.health.ToString();
        if (PermUI.perm.health <= 0)
        {
            PermUI.perm.ResetAll();
            Destroy(GameObject.FindWithTag("audioPlayer"));
            SceneManager.LoadScene("MainHub");

        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        if (hDirection < 0) //Left
        {
            transform.localScale = new Vector2(-playerSize, playerSize);

        }
        else if (hDirection > 0) //Right
        {
            transform.localScale = new Vector2(playerSize, playerSize);
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;


        //Jumping

        

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump(jumpForce);
            if (PermUI.perm.greek == true)
            {
                canJump = true;
            }
        }
        else if (canJump && Input.GetButtonDown("Jump"))
        {
            Jump(jumpForce2);
            //jumpAmt--; 
            canJump = false;
        }


    }

    private void Jump(float jp)
    {
        rb.velocity = new Vector2(rb.velocity.x, jp);
        state = State.jumping;
    }

    private void VelocityState()
    {
        if (state == State.jumping || (state != State.jumping && coll.IsTouchingLayers(ground) == false))
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
       
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
               state = State.idle;               
            }
        }
        else if (Input.GetAxis("Horizontal") != 0 && coll.IsTouchingLayers(ground) == true)
        {
            //Moving
            state = State.running;
        }
        else if (state == State.hurt)
        {
            
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else
        {
            state = State.idle;
        }

    }

    IEnumerator colorChanger()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Renderer>().material.color = Color.white;


    }

    void Attack()
    {
        
        if (coll.IsTouchingLayers(ground) == false)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                anim.SetTrigger("downdair");
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                anim.SetTrigger("upair");
            }
            else
            {
                anim.SetTrigger("inair");
            }
                
        }
        else if (state == State.running)
        {
            anim.SetTrigger("dashatk");
            //float x = transform.position.x;
            //float y = transform.position.y;
            //transform.position = new Vector2(x + 1, y);
        }
        else
        {
            anim.SetTrigger("atk");
        }



 

        if (Input.GetAxis("Vertical") < 0 && coll.IsTouchingLayers(ground) == false)
        {
            Collider2D[] downhitEnem = Physics2D.OverlapCircleAll(DairPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in downhitEnem)
            {
                Enemy enemy1 = enemy.gameObject.GetComponent<Enemy>();
                Debug.Log("dair");
                enemy1.Attacked();
                Jump(jumpForce);
                canJump = true;

            }
        }
        else if (Input.GetAxis("Vertical") > 0 && coll.IsTouchingLayers(ground) == false)
        {
            Collider2D[] uphitEnem = Physics2D.OverlapCircleAll(UairPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in uphitEnem)
            {
                Debug.Log("uair");
                Enemy enemy1 = enemy.gameObject.GetComponent<Enemy>();
                enemy1.Attacked();
            }
        }
        else
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("hit");
                Enemy enemy1 = enemy.gameObject.GetComponent<Enemy>();
                enemy1.Attacked();

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        
        if (DairPoint == null)
        {
            return;
        }
        if (UairPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(DairPoint.position, attackRange);
        Gizmos.DrawWireSphere(UairPoint.position, attackRange);
    }



    void Zero()
    {
        rb.velocity = Vector3.zero;
    }

    private IEnumerator ResetPower() //timer
    {
        yield return new WaitForSeconds(10);
        cherry = 0;
        GetComponent<SpriteRenderer>().color = Color.white; //white is default
    }

    private IEnumerator boarAttack() //timer
    {
        playerSize = .75f;
        jumpForce *= .75f;
        speed *= .75f; 

        yield return new WaitForSeconds(3);
        // scaleChange2 = new Vector3(1f, 1f, 0);
        //this.transform.localScale = scaleChange2;
        playerSize = 1f;
        jumpForce /= .75f;
        speed /= .75f;

    }


}
