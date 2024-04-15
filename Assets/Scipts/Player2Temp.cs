using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Temp : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    public int maxHealth = 100;
    public int currentHealth;
    private bool punch;
    private bool block;
    private bool win;
    private bool struck;
    private bool dead;


    [SerializeField]
    private float movementSpeed;
    private bool facingRight;


    private Rigidbody2D myRigidbody;



    public Healthbar healthBar;

    private void Start()
    {
        facingRight = true;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
        /*
         animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

         Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);

         transform.position = transform.position + horizontal * Time.deltaTime;
        */

        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);
        Flip(horizontal);
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }
    private void HandleMovement(float horizontal)
    {

        //  myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); 
      /*  if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("StraightPunch"))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsTag("StraightPunch"))
        {
            myRigidbody.velocity = new Vector2(0, 0);
        }

        animator.SetFloat("speed", Mathf.Abs(horizontal));
      */


        if (struck && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Struck"))
        {
            animator.SetBool("struck", true);
            TakeDamage(5);
        }
        else if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Struck"))
        {
            animator.SetBool("struck", false);
        }

    }


    private void HandleAttacks()
    {
        if (punch)
        {
            animator.ResetTrigger("punch");
            animator.SetTrigger("punch");
        }
    }


    private void HandleBlocks()
    {
        if (block)
        {
            animator.ResetTrigger("block");
            animator.SetTrigger("block");
        }
    }



    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            punch = true;
        }
        else
        {
            punch = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            block = true;
        }
        else
        {
            block = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            struck = true;
        }
        else
        {
            struck = false;
        }

    }


    private void ResetValues()
    {
        punch = false;
        block = false;
        win = false;
        struck = false;
    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

}

