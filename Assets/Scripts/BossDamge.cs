using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamge : MonoBehaviour
{
    public int damage = 50;
    public GameObject ps, boss;
    public NavMeshAi_Boss naiboss;
    public float explosionDelay = 2f; // 爆炸延遲時間

    void Start()
    {
        naiboss = boss.GetComponent<NavMeshAi_Boss>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
           PlayerHealth ph = other.GetComponent<PlayerHealth>();
             if (ph != null)
        {
            StartCoroutine(ExplodeAndDamageDelayed(other,ph));
            }
        }
    }

    IEnumerator ExplodeAndDamageDelayed(Collider playerCollider,PlayerHealth ph)
    {
        yield return new WaitForSeconds(explosionDelay);

        
        if (ph != null)
        {
            ph.TakeDamage(damage);
            PlayExplosionEffect();
        }
    }

    void PlayExplosionEffect()
    {
        if (ps != null)
        {
            Instantiate(ps, transform.position, Quaternion.identity); // 在Boss位置實例化爆炸特效
        }
        
        if (naiboss != null)
        {
            naiboss.enemyAttackBoomEnd();
        }
    }
}