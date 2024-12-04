using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eshoot : MonoBehaviour
{


    public bool isFire = false;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime;

    public float detectionRadius = 10f;
    public Transform[] targets;
    private Transform currentTarget;
    // Start is called before the first frame update
    void Start()
    {
         GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        targets = new Transform[playerObjects.Length];
        for (int i = 0; i < playerObjects.Length; i++)
        {
            targets[i] = playerObjects[i].transform;
        }
      
        
    }

    // Update is called once per frame
    void Update()
    {
         float closestDistance = Mathf.Infinity;
        for (int i = 0; i < targets.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, targets[i].position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                currentTarget = targets[i];
            }
        }
        if (currentTarget != null && closestDistance < detectionRadius)
        {
            transform.LookAt(currentTarget);
        }

           if (isFire&&Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
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
