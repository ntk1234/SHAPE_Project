using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float damageCooldown = 0.5f; // Cooldown time for taking damage
    private float damageCooldownTimer = 0f; // Timer for damage cooldown

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (damageCooldownTimer > 0)
        {
            damageCooldownTimer -= Time.deltaTime;
        }

        
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"Player healed. Current health: {currentHealth}");
    }

    public void TakeDamage(int damage)
    {
        if (damageCooldownTimer <= 0)
        {
            currentHealth -= damage;
            damageCooldownTimer = damageCooldown; // Start the damage cooldown

            if (currentHealth <= 0f)
            {
                Die();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            TakeDamage(enemyHealth.damage);
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        Destroy(gameObject, 1);
    }
}