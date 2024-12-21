using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitpower : MonoBehaviour
{
    
    public int playerID = 1; 
     public int damage = 10;
    
    public GameObject player1;

     public GameObject player2;
    public CharController cc;

    public CharController1 cc1;

    public AudioClip AudioEffect;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioEffect;

        if (player1 != null)
            {
                cc = player1.GetComponent<CharController>();
            }

            if(player2 != null)
            {
                cc1 = player2.GetComponent<CharController1>();
            }
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
      
            enemy.playerID = playerID ;
        }
         if (cc!=null&&collision.gameObject.CompareTag("Enemy")&&cc.isPunch)
        {
            cc.currMp+= 1;
            audioSource.Play();
            Debug.Log($"1pEng{cc.currMp}");
        }

         if (cc1!=null&&collision.gameObject.CompareTag("Enemy")&&cc1.isPunch)
        {
            cc1.currMp+= 1;
            audioSource.Play();
            Debug.Log($"2pEng{cc1.currMp}");
        }
        
    }
}
