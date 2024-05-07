# Projectwork day 4 Jumping and Hitting
Since I have been busy with my other student classes and needed to do a Bachelor's project and another project before the end of this month I have not reached the goal I wanted so far. 
As such I will use the last days of june before hand in to fix my project. 

Today I implemented the jump function and fixed the rigidbodies for the characters and added structures to the stage so the characters can't be moved out. However my first attempt at making a hitbox detection with my punch animation was unsuccesful.
I suspect that my hitbox is not detecting the other character's box collider and when hitting is therefore not activating the destroy.gameobject function or my takeDamage function. 

## Jump function
The jump function was created by making the fields written underneath and the function afterwards to help detect when the character is grounded so they can only jump when one of the three ground points are touching the ground.
```
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
```
It ended up looking like this.

https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/74ccb50d-f725-4b06-8a90-7b556eafa558


## hitting attempt 

The idea here was that the animater could detect the hitbox with the GetComponent for the box collider and invoke the calls for activating and deactivating the hitboxes.
```
public class MeleeAttack : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D hitbox;

    private void Start()
    {
        animator = GetComponent<Animator>();
        hitbox = transform.Find("Hitbox").GetComponent<BoxCollider2D>();
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Attack on Space key press.
        {
            animator.SetTrigger("punch");
            Invoke("ActivateHitbox", 1f); // Activate hitbox after 0.2 seconds.
            Invoke("DeactivateHitbox", 1f); // Deactivate hitbox after 0.4 seconds.
        }
    }
    
    void ActivateHitbox()
    {
        hitbox.gameObject.SetActive(true);
    }

    void DeactivateHitbox()
    {
        hitbox.gameObject.SetActive(false);
    }

```

## logic on hit 

In a perfect world the hitbox would be detected and the logic underneath would be applied just to test if the sprite would disappear or at least take damage in the UI
```

    public Healthbar healthBar;

    public int maxHealth = 100;
    public int currentHealth;

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2"))
        {
            // Damage or destroy the enemy.
            TakeDamage(5);
            Destroy(collision.gameObject);

        }
    }

```

Sadly this didn't work..

As  the functionality to punch and cause damage is the core aspect of a fighting game I should probably reach out to one of the supervisors or more help from the internet to gain some help on why my punching mechanic is not working atm

## Today
- [x] Implement jump and jump attack
- [x] Fix the rigidbodies on characters 
- [ ] Implement box collision for attacks and block

## Next Time
- [ ] Fix the attacks to actually detect box collision on the other player 
- [ ] Add sound when you finally detect collision
- [ ] Add the function to change character
