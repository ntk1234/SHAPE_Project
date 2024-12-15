using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharController1 : MonoBehaviour
{

    private  CharacterController characterController;
    private Animator animator;

     public float rotSpeed = 10;
    Vector3 moveDirection;

     public float kickRate = 3f;            // 踢撃頻率
        public  float kickTimer;  
                        // 踢撃計時器
    
    
     public float fightRate = 1f;            // 打撃頻率
        public  float fightTimer;  
                        // 打撃計時器

    public bool isPunch = false;

    public bool isKick = false;

      public int maxMp = 10;

        public int currMp ;
        public int kickMp = 5;
 
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        kickTimer = kickRate;
       fightTimer = fightRate;
      
    }

    // Update is called once per frame
    void Update()
    {

        currMp = Mathf.Clamp(currMp,0,maxMp);

         float h = 0;
        float v = 0;

        if (Input.GetKey("left"))
        {
            h = -1;
        }
        else if (Input.GetKey("right"))
        {
            h = 1;
        }

        if (Input.GetKey("down"))
        {
            v = -1;
        }
        else if (Input.GetKey("up"))
        {
            v = 1;
        }
        Vector2 move = new Vector2(h, v);
        animator.SetFloat("speed", move.magnitude);

        moveDirection = new Vector3(h, 0, v);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;

        kickTimer += Time.deltaTime;//踢撃時間器運作

        fightTimer += Time.deltaTime;//打撃時間器運作

        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotSpeed * Time.deltaTime);
        }

       if (fightTimer >= fightRate)
         {
            fightTimer=fightRate;
        }
        if (Input.GetKeyDown("b")&& fightTimer >= fightRate&&!isPunch&&!isKick)
        {
           isPunch=true;
            animator.SetTrigger("punch");
            fightTimer=0;
        } 
          if (kickTimer >= kickRate)
         {
            kickTimer=kickRate;
         }
        if (Input.GetKeyDown("n") && kickTimer >= kickRate&&!isPunch&&!isKick&&currMp>=kickMp)//踢撃條件達成
        {
            
           isKick=true;
           currMp-=kickMp;
            animator.SetTrigger("kick");
            kickTimer = 0f;
            
        }
         

       // if (Input.GetKeyDown("l") && hkickTimer >= hkickRate)//踢撃條件達成
       // {
            
        //    animator.SetTrigger("hkick");
      //      hkickTimer = 0f;
            
       // }

        
    }
}
