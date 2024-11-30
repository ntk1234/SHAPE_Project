using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAi : MonoBehaviour
{

     private NavMeshAgent agent;

     public Transform target;
     
    // Start is called before the first frame update
    void Start()
    {
         agent = GetComponent<NavMeshAgent>();
         //agent.destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
    
                    agent.destination = target.position;
            
    }
    
}
