using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPbullet : MonoBehaviour

{


    public int damage = 20;

    public float destroytime = 0.2f;
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
        
        if (other.CompareTag("Enemy"))
        {
       EnemyHealth eh = other.GetComponent<EnemyHealth>();
        if (eh != null)
        {
            eh.TakeDamage(damage);
            Destroy(gameObject,destroytime);
        }

        }
       // if (collision.gameObject.CompareTag("Enemy"))
       // {
       //      Destroy(gameObject);
       // }

         if (other.CompareTag("Map"))
        {
             
           
            Destroy(gameObject);
        }
    }

   
}
