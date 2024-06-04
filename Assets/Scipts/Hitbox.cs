using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

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
}
