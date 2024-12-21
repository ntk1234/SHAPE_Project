using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamge : MonoBehaviour
{
    public int damage = 50;
 
   

    void Start()
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
    }


   
}