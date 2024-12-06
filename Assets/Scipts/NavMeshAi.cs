using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAi : MonoBehaviour
{
    private NavMeshAgent agent;

    private Animator animator;
    public Transform[] targets;
    private Transform currentTarget;
    public Eshoot es;

    void Start()
    {

        animator = GetComponent<Animator>();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        targets = new Transform[playerObjects.Length];
        for (int i = 0; i < playerObjects.Length; i++)
        {
            targets[i] = playerObjects[i].transform;
        }

        agent = GetComponent<NavMeshAgent>();
    }

                void Update()
            {
                if (currentTarget == null || !currentTarget.gameObject.activeSelf)
                {
                    FindNextTarget();
                }

                if (currentTarget != null && agent.isOnNavMesh)
                {
                    
                    animator.SetFloat("speed",1);
                    agent.destination = currentTarget.position;

                    if (Vector3.Distance(transform.position, currentTarget.position) <= agent.stoppingDistance + 2)
                    {
                        animator.SetFloat("speed",0); 
                       animator.SetTrigger("emAttack");
                      
                    }
                 //  if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
                     //  {
                    //       animator.SetFloat("speed",0);
                    //   }
                }

                
               
            }

            void FindNextTarget()
            {
                float closestDistance = Mathf.Infinity;
                bool targetDestroyed = false;

                for (int i = 0; i < targets.Length; i++)
                {
                    if (targets[i] == currentTarget && targets[i] == null)
                    {
                        targetDestroyed = true;
                        break;
                    }

                      if (targets[i] == null) // Check if the target is null
                        {
                            continue; // Skip this target and move to the next one
                        }

                    float distance = Vector3.Distance(transform.position, targets[i].position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        currentTarget = targets[i];
                    }
                }

                if (targetDestroyed && targets.Length > 1)
                {
                    currentTarget = targets[1];
                  
                }
            }

            void enemyFire()
            {
             es.isFire = true;
            }

 
}