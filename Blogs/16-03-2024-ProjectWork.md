#Projectwork day 1. Movement

Today I finally started doing some work on my 2d fighting game project. 

As I didn't realise the big differences between 2d and 3d I struggled a lot as I only had my youtube videos to learn from. 

Since I had to do all the background and actually implement a player character as a sprite if took me quite some time to actually get a fitting background and make all the frames for my scissors character. 
Which will be the initial character for starrt up. 

![Skærmbillede 2024-03-16 141820](https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/42b3a8e7-3230-4718-a9ab-fe7d25fe7db3)


Furthermore, I struggled a lot with the camera as the videos I saw didn't make sense with my work and I became very furious as I couldn't see my character 

As I was mainly focused on doing simple movement today I only added this code below. But had to do a lot of settings and animation videos.

```
void Update()
    {
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);

        transform.position = transform.position + horizontal * Time.deltaTime;
    }
```

However, even though it looks quite goofy my character could walk back and forth on screen with right and left click. 

https://github.com/Charliecoolswag/FightingGameProject/assets/75067860/703ec096-e6e9-42a0-90ee-37a8ec57386d 

Now that I have a better view on how 2d differs from 3d and how animations should be used with the animator components I would like to do some hitting and blocking animation and the Healthbar UI tomorrow.

## Today
- [x] Background and first character 
- [x] Basic movement

## Tomorrow
- [ ] Healthbar UI 
- [ ] Hitting and punching
