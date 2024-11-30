using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitpower : MonoBehaviour
{

     public int damage = 10;
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
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            //Destroy(gameObject);
        }
        
    }
}
