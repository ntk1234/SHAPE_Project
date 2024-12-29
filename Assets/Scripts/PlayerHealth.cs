using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float damageCooldown = 0.5f; // Cooldown time for taking damage
    private float damageCooldownTimer = 0f; // Timer for damage cooldown

     public float def = 0 ; 

     public float defTimer;

     public float defRate=5f;

     public bool isdefup=false;

    public int playerID =0;

    //public GameObject gm;

    public ReviveHealthManger rhm;

    public GameObject defeffect;
    
    public Shop shop;

   

    void Start()
    {
        currentHealth = maxHealth;
        defeffect.SetActive(false);
        rhm= GameObject.Find("GameManger").GetComponent<ReviveHealthManger>();
        shop = GameObject.Find("GameManger").GetComponent<Shop>();
    }

    void Update()
    {
        if (damageCooldownTimer > 0)
        {
            damageCooldownTimer -= Time.deltaTime;
        }
        if(isdefup){
         defTimer += Time.deltaTime;
        }
        if (defTimer >= defRate&&isdefup)
         {
            def -= 50f;
            defTimer=0;
             Debug.Log($"Player defdown. def: {def}");
           defeffect.SetActive(false);
          
            isdefup=false;
            
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"Player healed. Current health: {currentHealth}");
    }

    public void DefUP(float amount)
    {   
        isdefup=true;
        def += amount;
        Debug.Log($"Player defup. def: {def}");
        if (defeffect != null)
        {
            defeffect.SetActive(true);
         
        }
        
    }
    
    public void TakeDamage(int damage)
    {
        if (damageCooldownTimer <= 0)
        {
            currentHealth -= damage-def;
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

        if(playerID == 1){
        rhm.lastPlayer1Position = transform.position;
        shop.re1pBtnObj.SetActive(true);
        shop.isBuy1Pre = false;
        }
        else if (playerID == 2)
        
        {rhm.lastPlayer2Position = transform.position;
        shop.re2pBtnObj.SetActive(true); 
        shop.isBuy2Pre = false;
        }

        Destroy(gameObject, 1);

        
    }
}
