using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombieattackupdate: MonoBehaviour

{



    
    public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
          
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
        PlayerHealth ph = other.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
        }

        }
       // if (collision.gameObject.CompareTag("Enemy"))
       // {
       //      Destroy(gameObject);
       // }

      
    }

   
}
