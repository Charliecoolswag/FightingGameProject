using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;  // Add this namespace for TextMeshPro

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public GameObject winTextObject;
    public TextMeshProUGUI countdownText; // Add this line to reference the TextMeshPro UI component

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

    private AudioSource source;
    public AudioClip hitClip;
    public AudioClip youWin;
    public AudioClip impactClip;

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

    private bool canProcessInput = true; // Add this line to control input processing

    private void Start()
    {
        winTextObject.SetActive(false);

        facingRight = true;
        P1CurrentHealth = P1MaxHealth;
        P2CurrentHealth = P2MaxHealth;
        healthBarP2.SetMaxHealth(P2MaxHealth);

        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        source = GetComponent<AudioSource>();

        countdownText.gameObject.SetActive(false);  // Initially hide the countdown text
    }

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
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

    void Update()
    {
        if (!canProcessInput) return; // Add this line to skip input processing if input is disabled

        HandleInput();
        HandleBlocks();

        if (P2CurrentHealth <= 0 && !win)
        {
            win = true;
            SetWin();
            winTextObject.SetActive(true);

            // Disable input
            canProcessInput = false;

            // Start the coroutine to reload the scene after a delay
            StartCoroutine(ReloadSceneAfterDelay(10f));  // Delay set to 10 seconds
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
            source.PlayOneShot(hitClip);
        }
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        countdownText.gameObject.SetActive(true);  // Show the countdown text

        int remainingTime = Mathf.CeilToInt(delay);
        while (remainingTime > 0)
        {
            countdownText.text = $"Reloading in {remainingTime} seconds";
            yield return new WaitForSeconds(1f);  // Update the text every second
            remainingTime--;
        }

        countdownText.text = "Reloading now...";
        yield return new WaitForSeconds(1f);  // Wait a bit before reloading

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void attack()
    {
        Collider2D[] P2Damage = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
        foreach (Collider2D enemyGameobject in P2Damage)
        {
            Debug.Log("Hit enemy");
            TakeDamageP2(10);
            source.PlayOneShot(impactClip);
        }

        Collider2D[] P1Damage = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
        foreach (Collider2D enemyGameobject in P1Damage)
        {
            Debug.Log("Hit enemy");
            TakeDamageP1(10);
        }
    }

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

    private void HandleMovement(float horizontal)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("isAttacking"))
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsTag("isAttacking"))
        {
            myRigidbody.velocity = new Vector2(0, 0);
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
        block = false;
        win = false;
        struck = false;
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
        source.PlayOneShot(youWin);
    }
}
