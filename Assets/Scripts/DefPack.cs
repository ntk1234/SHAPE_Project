using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefPack : MonoBehaviour
{
    public float def = 50f; // Amount of defense to add
    private AudioSource soundEffects;

 

    void Start()
    {
        soundEffects = GetComponent<AudioSource>(); // Access the AudioSource component correctly
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.DefUP(def);

                if (soundEffects != null)
                {
                    soundEffects.Play();
                }
                 Destroy(gameObject);
               
                
            }
        }
    }

}