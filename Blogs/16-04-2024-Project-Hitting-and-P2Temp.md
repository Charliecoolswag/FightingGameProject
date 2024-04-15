# Projectwork day 3 Hitting and P2
Since it had been such a long time since I made the last entry and work on the project. I almost forgot what I had to do which was to implement the hitting and player 2 character or at least the start up on it. 

Since I am not fully done structuring my Player 1 character I simply just copied that gameobject and gave him an almost identical script just to test hitboxes and animation behavior in another entry. 
Which is due to the fact that my Player 2 character will not have an AI and only be a player character. Therefore needed almost identical script to player 1 except controls 

As I reconsidered my previous work I also spent some time rewriting my old script to just use a flip method that transforms the x scale of my sprite to negative number so that I can move the character to both sides and simulate hitting on both sides. 
Which I did now as I was afraid later on when I will implement jump that they needed some other way to hit eachother if they ended on the opposite site. Instead of the traditional way where the fighter characters are always facing eachother

```
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
```
The punch and blocking animation was then scripted as a trigger function in the script and its input values set in the HandleInput constructor that is called in Update() 

```
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
```
Lastly I tried to shortly implement the struck animation on keypress R and shot that they take damage in the hud which of course later will be set to true with the condition that the characters boxcolliders has been hit instead. 


```
if (struck && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("Struck"))
        {
            animator.SetBool("struck", true);
            TakeDamage(5);
        } else if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Struck"))
        {
            animator.SetBool("struck", false);
        }
```
Finally it all ended up looking like this

https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/ded1e42c-9c0d-4fca-aaa6-168f932d3e24

As seen in the end the box colliders on the characters arm moves on the attack animation but does not do anything atm. However I forgot to tweak the rigidbodies that at the moment has 0 gravity which made them move very goofy when they collided. 

The goal for next time is therefore to work and fix the rigidbodies and box colliders so that I can begin to make the logic for the attack damage, collision and block which will be the core gameplay of JanKen


## Today 
- [x] Hitting and punching
- [x] New Movement
- [x] Player2 temporary for testing

## Next Time
- [ ] Implement jump and jump attack
- [ ] Fix the rigidbodies on characters 
- [ ] Implement box collision for attacks and block

