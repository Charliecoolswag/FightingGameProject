using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;


    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private float jumpforce;

    private Rigidbody2D myRigidbody;
  

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <=0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position,groundRadius,whatIsGround);
                for (int i=0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
            
        }
        return false;
    }


    public Healthbar healthBar;

    private void Start()
    {
        facingRight = true;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        myRigidbody= GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        HandleInput();
        //HandleAttacks();
        HandleBlocks();
        
        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();
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

    // movement 
        private void HandleMovement(float horizontal)
    {
        
        //  myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); 
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("StraightPunch"))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsTag("StraightPunch"))
        {
            myRigidbody.velocity = new Vector2(0, 0);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpforce));
        }


        animator.SetFloat("speed", Mathf.Abs(horizontal)); 



        if (struck && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Struck"))
        {
            animator.SetBool("struck", true);
            TakeDamage(5);
        } else if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Struck"))
        {
            animator.SetBool("struck", false);
        }
        
    }

    /*
    private void HandleAttacks()
    {
        if (punch)
        {
            animator.ResetTrigger("punch");
            animator.SetTrigger("punch");
        }
    }
    */

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true; 
        } else { 
            jump = false; 
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            punch = true;
        } else {
            punch = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            block = true; 
        }  else { 
            block = false; }

        if (Input.GetKeyDown(KeyCode.R))
        {
            struck = true;
        } else  {
            struck = false;
        }

    }


    private void ResetValues()
    {
        punch = false;
        block= false;
        win= false;
        struck= false;
        jump = false;
    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

}
