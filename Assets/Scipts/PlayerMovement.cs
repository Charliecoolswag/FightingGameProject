using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public int maxHealth = 100;
    public int currentHealth;
    private bool attack;
    private bool block;

    [SerializeField]
    private float movementSpeed;

    private Rigidbody2D myRigidbody;



    public Healthbar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        myRigidbody= GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
           // animator.SetTrigger("Punch");
            TakeDamage(5);
        }

        HandleInput();
        HandleAttacks();
        HandleBlocks();


        /*
        if(transform.position.x <= -9) 
        {
            transform.position = new Vector3(transform.position.x, -2, 0);
        }
       */


         animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

         Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);

         transform.position = transform.position + horizontal * Time.deltaTime;
        
        /*
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);
        */
    }



    private void HandleMovement(float horizontal)
    {
        /*
        //  myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); 
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Punch"))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Punch"))
        {
            myRigidbody.velocity = new Vector2(0, 0);
        }

        animator.SetFloat("speed", Mathf.Abs(horizontal)); 
        */
    }


    private void HandleAttacks()
    {
        if (attack)
        {
            animator.ResetTrigger("Punch");
            animator.SetTrigger("Punch");
        }
    }
    

    private void HandleBlocks()
    {
        if (block)
        {
            animator.ResetTrigger("Block");
            animator.SetTrigger("Block");
        }
    }



    private void HandleInput() 
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            attack = true;
            
        } else {
            attack = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            block = true; 
        } else { block = false; }

    }



    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

}
