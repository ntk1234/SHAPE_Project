using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ebulletupdate : MonoBehaviour

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

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
            Destroy(gameObject);
        }
       // if (collision.gameObject.CompareTag("Enemy"))
       // {
       //      Destroy(gameObject);
       // }

         if (collision.gameObject.CompareTag("Map"))
        {
             Destroy(gameObject);
        }
    }
}
