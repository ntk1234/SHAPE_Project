using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPack : MonoBehaviour
{
    public float healingHP = 20f; // Amount of health to restore
    private AudioSource soundEffects;

    
    void Start()
    {
        soundEffects = GetComponent<AudioSource>();
    }

    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the player
        if (other.CompareTag("Player"))
        {
            // Attempt to get the PlayerHealth component
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Check if the player's health is below max
                if (playerHealth.currentHealth < playerHealth.maxHealth)
                {
                    // Add health to the player
                    playerHealth.Heal(healingHP);

                    // Play sound effect if available
                    if (soundEffects != null)
                    {
                        soundEffects.Play();
                    }

                    // Destroy the healing pack after the sound has played
                    // Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Player already at maximum health. Healing not applied.");
                }
                Destroy(gameObject);
            }
        }
        
    }
}
