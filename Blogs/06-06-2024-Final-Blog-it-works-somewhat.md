# Projectwork Final Day Showcase 
As I finally finished my Bachelor Project I had to do a lot of work on this last sprint to make the game playable with two characters using the player input system and actually implement hit and win mechanics for the game.
I fortunately made the game able to be played with one joystick on the arcade machine and keyboard with the scissor characters only. Which unfortunately was not fully what I wanted for the game but I sadly had to prioritize since I had to so much this week and also have another exam the day after tomorrow that I had prepare for.  


This week I finally implemented hit detection for the attacks, a jump with same mechanic, two player mode, sound effects and a win condition that resets the scene. Which should be enough to demonstrate the idea of a fighting game on the arcade machine. 


## Hit function
The previous hit detection method was scratched as it didn't activate the hitbox and use the takeDamage function I previously made. As such an easier example that works well with 2d platformers was used. Which was to use collider detection based on key frames and layers. 

By activating a circle in the attack method that detects all layers in the P2damage arrays that is looped to detect any objects in the public layermask enemies. 


Using detection this way I could easily implement my logic inside it to activate only when it detects the specified layer in the enemies layer mask. Such as the takedamage from earlier and the impact sound effects. 
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
The circle is then activated during a key frame on the hit as specified below

![image](https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/221ac5b4-076b-4640-8c52-9a68c9147ba3)




### attack called, damage and public fields 
As seen on the picture two new empty game objects were made an applied to the parent character object which are the points that activates a circle to detect layers in the enemies layermask. Which in this example for player2 is the P1 layer that the player 1 character has and vice versa with player 2. The picture also has other serialized fields that is referenced later  
![image](https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/5d611402-8ecb-4e17-9782-c8e41503644e)
![image](https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/edc701a6-f871-4393-9449-367556e5aa9e)

As I also had to change the controls to use the player input manager I also had to change my controls to use a specified button schematics. Here called attack and make the attack animation that was ealier a trigger to a boolean and set the attack to start on the attack call in update and have another method that ends the attack, that is called by the animation event in the end of the animation to end the attack.
```
bool attack = playerInput.actions["Attack"].WasPerformedThisFrame();
void Update()
{
if (attack && isGrounded)
  {
    animator.SetBool("isAttacking", true);
    source.PlayOneShot(hitClip);
  }
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

Since I had successfully implemented a hit I could easily implement a kick function that only works when the character is not grounded. As such the same code structure was used with some changes. 
```
public GameObject kickPoint;

bool jump = playerInput.actions["Jump"].WasPerformedThisFrame();
bool kick = playerInput.actions["Attack"].WasPerformedThisFrame();
 if (isGrounded && jump)
 {
     isGrounded = false;
     myRigidbody.AddForce(new Vector2(0, jumpforce));
 }
 animator.SetFloat("speed", Mathf.Abs(horizontal));

 isGrounded = IsGrounded();

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
To make sure that the kick is only applied during air with the same button as attack. The isGround method was uses as a parameter in the if statement for attack and kick to make sure only the right animation was played based on the character's gameobjects was colliding with anything. In this example the kick is applied if !IsGrounded. Which looked like this

https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/5281dc68-8720-4adf-824d-a6abb327c526



## Win condition 

Now that the main functionalities to win are in place. A win screen and a way to reset the game to replay was needed. Here GUI objects were implemented. 
Two TextMeshProUGUI objects were applied in the canvas and turned off during start up. Which were displayed based on P2CurrentHealth <=0 in update to show that a character had won. 
```
public GameObject winTextObject;
public TextMeshProUGUI countdownText; // Add this line to reference the TextMeshPro UI component
   private void Start()
{
    winTextObject.SetActive(false);
countdownText.gameObject.SetActive(false);  // Initially hide the countdown text

}
void Update()
{
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
        }

```

To restart the scene and implement a countdown for restarting a coroutine was implemented since the game needed to restart based on a specified delay. The coroutine starts a while loop based on remainingtime > 0. Which displays the countdownText with a specified text and the number is updated every second. 
after the while loop is closed the scene is then reloaded with sceneManager which reads the current scene and loads it again. This all called in the if statement in the update for (P2currentHealth <=0) 

```
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
 void SetWin()
 {
     animator.SetBool("win", true);
     Debug.Log("Win animation triggered in PlayerMovement");
     source.PlayOneShot(youWin);
 }

```
it ended up looking like this

https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/1d0be702-210a-4777-946a-ed2e2527d092


### The whole project showcase video can be seen here
https://www.youtube.com/watch?v=-2bkoOsNhAw 

## Conclusion and considerations 
The game I initially wanted is still some miles ahead to be implemented. As I originally wanted to have different characters rock and paper appear which should have different logic for attacks such as takedamage being 30 for paper attacking someone with the layer rock. 
The unique idea of the game was sadly not realised. I had to comprimise a lot during the development of this project as I had to learn of lot the implementations while during the different blogs which were not based on fighting games.
Furthermore I didn't practise using the player input manager and prefabs for my development which were important for the assignment to work on the arcade machine which was something I did VERY late in the process resulted in having to restructure a lot of the code and do additinal testing. As such I didn't have time to figure out how to map the another joystick to work and two player mode only works with 1 joystick and keyboard. Something I also only first realised when testing it on the machine the day before.

Lastly as I had difficulties understanding the concept of interfaces and prefabs in unity I didn't practice the solid principles especially single responsibility and dependcy inversion principles. 
As I did not know that earlier that prefabs could be changed individueally. I was under the impression that I had to make both characters have access to the same functions and do some duplicate code to give them both their own healthbar. Which is definitely not a scalable way to structure to different characters. 
Since I didn't put my methods in different classes and attach it to the character instead from the start. My playermove script was also way too long, A mistake I made as early as the beginning since I thought I would just divide the script based ont the three different characters I wanted to make. Instead of just dividing shared functions such as the healthbar and attack functions to have single responsibilies not make the prefabs dependent on the same methods. 

However, despite this flaw. I still managed to make a game that could be played to simulate a fighting game that can be partly played on the arcade machine. Though a very limited one which is much more boring than my initial idea. 


## This week
- [x] Fix the attacks to actually detect box collision on the other player 
- [x] Add sound when you finally detect collision
- [x] Implement box collision for attacks and block
- [ ] Implement win condition
- [ ] Implement two player mode

## Missing functions
- [ ] Add the function to block character with layer switch or other invincibility frame mechanic
- [ ] Add A menu
- [ ] Add the function to change character
- [ ] Add animation behavior when charater is hit
