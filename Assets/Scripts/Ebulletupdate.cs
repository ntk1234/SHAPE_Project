using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ebulletupdate : MonoBehaviour

{

    public ParticleSystem particleSystemToDestroy,particleSystemToDestroy2;

    public GameObject ps1,ps2;

    
    public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        particleSystemToDestroy =ps1.GetComponent<ParticleSystem>();
        particleSystemToDestroy2=ps2.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
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
            DestroyParticleSystem();
            Destroy(gameObject);
        }

        }
       // if (collision.gameObject.CompareTag("Enemy"))
       // {
       //      Destroy(gameObject);
       // }

         if (other.CompareTag("Map"))
        {
             
            DestroyParticleSystem();
            Destroy(gameObject);
        }
    }

    void DestroyParticleSystem()
    {
        if (particleSystemToDestroy != null&&particleSystemToDestroy2 != null)
        {
            // 停止粒子系統播放
            particleSystemToDestroy.Stop();
            particleSystemToDestroy2.Stop();
             Debug.Log("PT STOP");
            // 延遲一幀後銷毀粒子系統物件
            Destroy(particleSystemToDestroy.gameObject, 0.1f);
            Destroy(particleSystemToDestroy2.gameObject, 0.1f);
        }
    }
}
