using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public GameObject winTextObject;

    public HealthbarP1 healthBarP1;
    public int P1MaxHealth = 100;
    public int P1CurrentHealth;

    public HealthbarP2 healthBarP2;
    public int P2MaxHealth = 100;
    public int P2CurrentHealth;
    private bool block;
    private bool win;
    private bool struck;
    private bool dead;


    public GameObject attackPoint;
    public float radius;
    public LayerMask enemies;



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


    [SerializeField] PlayerInput playerInput;





    private void Start()
    {
        winTextObject.SetActive(false);


        facingRight = true;
        P1CurrentHealth = P1MaxHealth;
        P2CurrentHealth = P2MaxHealth;
        healthBarP2.SetMaxHealth(P2MaxHealth);
       

        myRigidbody= GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

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


    


    // Update is called once per frame ------------------------------------------------------------------------
    void Update()
    {

        HandleInput();
        //HandleAttacks();
        HandleBlocks();


       if (P2CurrentHealth <=0 && !win)
        {
            win = true;
            SetWin();
            winTextObject.SetActive(true);
        }


        float horizontal = playerInput.actions["Move"].ReadValue<Vector2>().x;
        bool jump = playerInput.actions["Jump"].WasPerformedThisFrame();
        bool attack = playerInput.actions["Attack"].WasPerformedThisFrame();


        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpforce));
        }
        animator.SetFloat("speed", Mathf.Abs(horizontal));



        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);


        

        if (attack)
        {
            animator.SetBool("isAttacking", true);
        }

    }





    public void attack()
    {
        Collider2D[] P2Damage = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
        foreach(Collider2D enemyGameobject in P2Damage)
        {
            Debug.Log("Hit enemy");
            TakeDamageP2(10);
            
        }


        Collider2D[] P1Damage = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
        foreach (Collider2D enemyGameobject in P1Damage)
        {
            Debug.Log("Hit enemy");
            TakeDamageP1(10);

        }


    }

    /*
    public void isStruck()
    {
        animator.SetBool("struck", true);
    }
    */

    
    public void endAttack()
    {
        animator.SetBool("isAttacking", false);
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("isAttacking"))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsTag("isAttacking"))
        {
            myRigidbody.velocity = new Vector2(0, 0);
        }

        
        

        


        /*
        if (struck && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Struck"))
        {
            animator.SetBool("struck", true);
            TakeDamage(5);
        } else if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Struck"))
        {
            animator.SetBool("struck", false);
        }
        */
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
        block= false;
        win= false;
        struck= false;
        jump = false;
        
    }

    void TakeDamageP1(int damage)
    {
        P1CurrentHealth -= damage;

        healthBarP1.SetHealth(P1CurrentHealth);
    }


    void TakeDamageP2(int damage)
    {
        P2CurrentHealth -= damage;

        healthBarP2.SetHealth(P2CurrentHealth);
    }



    
    

    void SetWin()
    {
        animator.SetBool("win", true);
        Debug.Log("Win animation triggered in PlayerMovement");
       
    }


}
