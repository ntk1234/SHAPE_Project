using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour

{

    
    public float maxHealth = 10f; // 物件的最大生命值
    public float currentHealth;
    
    public int damage = 10;

    public int exp = 10 ;

    public Expupdate expupdate;
    
    public GameObject gm;

    
    // Start is called before the first frame update
    void Start()
    {
         currentHealth = maxHealth;
         expupdate = GameObject.Find("GameManger").GetComponent<Expupdate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  

     public void Die()
    {
          Debug.Log("Enemy died");
        Destroy(gameObject);
    }
      public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        if (currentHealth <= 0)
        {

           if (Random.value < 0.5f) // 50% chance
            {
                expupdate.currentExp += exp;
                Debug.Log("Enemy defeated and gained EXP!");
            }

             Die();
        }
        
    }

}
