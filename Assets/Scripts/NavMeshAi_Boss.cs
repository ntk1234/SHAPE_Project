using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAi_Boss: MonoBehaviour
{
    private NavMeshAgent agent;

   private Animator animator;
    public Transform[] targets;
    private Transform currentTarget;
    
    public GameObject bhitbox,remark,ps;

    private GameObject explosionInstance;

    public bool isstop =false;
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

        bhitbox.SetActive(false);
        remark.SetActive(false);
    }

                void Update()
            {
                if (currentTarget == null || !currentTarget.gameObject.activeSelf)
                {
                    FindNextTarget();
                }
                

                if (currentTarget != null && agent.isOnNavMesh)
                {
                    
                    if(!isstop){
                    animator.SetFloat("speed",1);
                  
                    agent.destination = currentTarget.position;}

                    if (Vector3.Distance(transform.position, currentTarget.position) <= agent.stoppingDistance+1)
                    {
                        isstop=true;
                        animator.SetFloat("speed",0); 
                        animator.SetTrigger("bossAttack");
                       
                       
                    }

                     if (Vector3.Distance(transform.position, currentTarget.position) > 5f)
                    {
                        isstop =false;
                      // animator.SetFloat("speed",0); 
                        FindNextTarget();
                    }


                }
                 
                
               
            }
            void FixedUpdate()
            {

                
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

           public void enemyAttackBoom()
           {
           
            bhitbox.SetActive(true);
    
           }
            public void enemyAttackBoomArea()
           {
           
             remark.SetActive(true);
    
           }

           public void enemyAttackBoomEnd()
           {
            bhitbox.SetActive(false);
            
    
           }

              public void enemyAttackBoomAreaEnd()
           {
           
             remark.SetActive(false);
    
           }

        public void PlayExplosionEffect()
        {
            if (ps != null)
            {
                explosionInstance = Instantiate(ps, transform.position, Quaternion.identity); // 在Boss位置實例化爆炸特效
                Destroy(explosionInstance, 3f);
            }
            
        
        }
 
}