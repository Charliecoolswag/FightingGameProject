# Projectwork Final Day Showcase 
As I finally finished my Bachelor Project I had to do a lot of work on this last sprint to make the game playable with two characters using the player input system and actually implement hit and win mechanics for the game.
I fortunately made the game able to be played with one joystick on the arcade machine and keyboard with the scissor characters only. Which unfortunately was not fully what I wanted for the game but I sadly had to prioritize since I had to so much this week and also have another exam the day after tomorrow that I had prepare for.  


This week I finally implemented hit detection for the attacks, a jump with same mechanic, two player mode, sound effects and a win condition that resets the scene.


## Hit function
The jump function was created by making the fields written underneath and the function afterwards to help detect when the character is grounded so they can only jump when one of the three ground points are touching the ground.
```
 public GameObject attackPoint;
 public GameObject kickPoint;
 public float radius;
 public LayerMask enemies;

   public void attack()
{
    Collider2D[] P2Damage = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);
    foreach (Collider2D enemyGameobject in P2Damage)
    {
        Debug.Log("Hit enemy");
        TakeDamageP2(10);
        source.PlayOneShot(impactClip);
        source.PlayOneShot(scissorClip);
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


```

![image](https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/5d611402-8ecb-4e17-9782-c8e41503644e)
![image](https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/edc701a6-f871-4393-9449-367556e5aa9e)

```
if (attack && isGrounded)
{
    animator.SetBool("isAttacking", true);
    source.PlayOneShot(hitClip);
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
```
It ended up looking like this.


https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/cbc31b4c-17a4-4cf0-832d-31ac4b0a065c



## Kick

The idea here was that the animater could detect the hitbox with the GetComponent for the box collider and invoke the calls for activating and deactivating the hitboxes.
```
public GameObject kickPoint;

bool jump = playerInput.actions["Jump"].WasPerformedThisFrame();
if (kick && !isGrounded)
{
    animator.SetBool("isStruck", true);
    source.PlayOneShot(kickClip);
}

public void Kick()
{
    Collider2D[] P2Damage = Physics2D.OverlapCircleAll(kickPoint.transform.position, radius, enemies);
    foreach (Collider2D enemyGameobject in P2Damage)
    {
        Debug.Log("Kick enemy");
        TakeDamageP2(15);
        source.PlayOneShot(impactClip);
        source.PlayOneShot(scissorClip);
    }

    Collider2D[] P1Damage = Physics2D.OverlapCircleAll(kickPoint.transform.position, radius, enemies);
    foreach (Collider2D enemyGameobject in P1Damage)
    {
        Debug.Log("Kick enemy");
        TakeDamageP1(15);
    }
}
 public void endKick()
 {
     animator.SetBool("isStruck", false);
 }

```


https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/5281dc68-8720-4adf-824d-a6abb327c526





## Win condition and single script considerations and regrets 

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
![Skærmbillede 2024-03-16 141820](https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/4699f8e0-b285-4246-8d17-9fe368babf03)

Sadly this didn't work..

As  the functionality to punch and cause damage is the core aspect of a fighting game I should probably reach out to one of the supervisors or more help from the internet to gain some help on why my punching mechanic is not working atm


https://www.youtube.com/watch?v=-2bkoOsNhAw 

## Today
- [x] Implement jump and jump attack
- [x] Fix the rigidbodies on characters 
- [ ] Implement box collision for attacks and block

## Next Time
- [ ] Fix the attacks to actually detect box collision on the other player 
- [ ] Add sound when you finally detect collision
- [ ] Add the function to change character
