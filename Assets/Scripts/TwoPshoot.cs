using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPshoot: MonoBehaviour
{

 
    public bool isFire = false;

   
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    

 
  
    void Start()
    {
       
      
        
    }

    // Update is called once per frame
            void Update()
        {
           

           
            

            if (isFire )
            {
               
                FireBullet();
                isFire = false;
            }
        }

    void FireBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

      
}
