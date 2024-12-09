using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour
{

    private  CharacterController characterController;
    private Animator animator;

     public float rotSpeed = 10;
    Vector3 moveDirection;

     public float kickRate = 3f;            // 踢撃頻率
        public  float kickTimer;  
                        // 踢撃計時器
    
      public float hkickRate = 10f;            // 踢撃頻率
        public  float hkickTimer;  
    
     public float fightRate = 1f;            // 打撃頻率
        public  float fightTimer;  
                        // 打撃計時器

    public bool isPunch = false;

    public bool isKick = false;

     public bool isHKick = false;

        public Shop shop;
 
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        shop = GameObject.Find("GameManger").GetComponent<Shop>();
        kickTimer = kickRate;
        hkickTimer = hkickRate;
       fightTimer = fightRate;
    }

    // Update is called once per frame
    void Update()
    {
        float h = 0;
        float v = 0;

        if (Input.GetKey("a"))
        {
            h = -1;
        }
        else if (Input.GetKey("d"))
        {
            h = 1;
        }

        if (Input.GetKey("s"))
        {
            v = -1;
        }
        else if (Input.GetKey("w"))
        {
            v = 1;
        }
        Vector2 move = new Vector2(h, v);
        animator.SetFloat("speed", move.magnitude);

        moveDirection = new Vector3(h, 0, v);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;

        kickTimer += Time.deltaTime;//踢撃時間器運作

        hkickTimer += Time.deltaTime;

        fightTimer += Time.deltaTime;//打撃時間器運作

        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotSpeed * Time.deltaTime);
        }

       
        if (Input.GetKeyDown("j")&& fightTimer >= fightRate&&!isKick&&!isPunch&&!isHKick)
        {
            isPunch=true;
            animator.SetTrigger("punch");
            fightTimer=0;
        } 

        if (kickTimer >= kickRate)
         {
            kickTimer=kickRate;
         }

        if (Input.GetKeyDown("k") && kickTimer >= kickRate&&!isKick&&!isPunch&&!isHKick)//踢撃條件達成
        {
            
            isKick=true;
            animator.SetTrigger("kick");
            kickTimer = 0f;
            
        }
         
         if (hkickTimer >= hkickRate)
         {
            hkickTimer=hkickRate;
         }

        if (Input.GetKeyDown("l") && hkickTimer >= hkickRate&&!isKick&&!isPunch&&!isHKick&&shop.isBuyhkick)//踢撃條件達成
        {
            isHKick= true;
           animator.SetTrigger("hkick");
           hkickTimer = 0f;
            
        }

        
    }
}
