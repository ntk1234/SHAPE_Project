using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour

{
    public float maxHealth = 100f; // 物件的最大生命值
    public float currentHealth;
    //private HealingPack healing;


    // Start is called before the first frame update
    void Start()
    {
         currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Heal(float amount)
    {
        // Add health but ensure it does not exceed the maximum health
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"Player healed. Current health: {currentHealth}");
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // 減少當前生命值

        if (currentHealth <= 0f)
        {
          
            Die(); // 如果當前生命值小於等於 0，則執行死亡動作
        }
    }

    private void OnCollisionEnter(Collision other)
    {
       
            EnemyHealth  enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                 TakeDamage(enemyHealth.damage);
            
            }
        
    }

      

     private void Die()
    {
        Debug.Log("Char died");
        Destroy(gameObject, 1);
    }
}
