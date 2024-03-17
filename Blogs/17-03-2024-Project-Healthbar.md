
# Projectwork day 2 Healthbar

Today I tried to do the assignments and I had great success with making a healthbar that could be manipulated by button click with the slider function. 
Since it was function it was fairly easy to implement in the UI and only needed very few script lines in a new controller and a few calls in the PlayerMovement to make damage to the healthbar and of course some UI setup. 

Written below: 
### Code in the new Healthbar script

```
public Slider slider;
    

    public void SetMaxHealth(int health) 
    { 
        slider.maxValue= health;
        slider.value = health;
    }
    public void SetHealth(int health) 
    { 
        slider.value = health;
    }
```

### Code in Playermovement
```
   private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

  void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
```

It came out looking like this 

https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/b696726e-8d81-4be4-8dd7-f324aaac2051

As I also wanted to do a hitting animation I tried searching the web for answers how to do it. However the possibilites are vast and most people tend to do the physics and logics of hitting an object when doing this. 
As such I figured I might as well try to make my hitting mechanic when doing the hit animation, which would require me to implement the player 2 character with a form of rigidbodies so that they can interact with outeachother and lower eachothers healthbar. 

I will try to ask the supervisors tomorrow for inspiration for what would be optimal to do. 

## Today
- [x] Healthbar UI 
- [ ] Hitting and punching

## Next Time
- [ ] Ask Supervisors 
- [ ] Implement player character 2 







