using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ReviveHealthManger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player1,player2;

     public int playerID;

    public CanvasController canvasController;
  

    public Vector3 lastPlayer1Position,lastPlayer2Position;

      private Vector3 respwanPosition;

    private GameObject newplayer;

     public CinemachineTargetGroup targetGroup;
    

    private GameObject currentTarget; 

    public Shop shop;
    
    public GameManager gamemanger;

    public bool isReviving =false;

    void Start()
    {
        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
        targetGroup = GameObject.Find("Target Group").GetComponent<CinemachineTargetGroup>();
        shop = GameObject.Find("GameManger").GetComponent<Shop>();
        gamemanger = GameObject.Find("GameManger").GetComponent<GameManager>();;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RevivePlayer1P()
    {
            RevivePlayer(player1, 1);
           
      }
    
     public void RevivePlayer2P()
    {
            RevivePlayer(player2, 2);
           
      }


    public void RevivePlayer(GameObject player ,int playerID)
    {
      
        StartCoroutine(ReviveSequence(player,playerID));
        
    }

    private IEnumerator ReviveSequence(GameObject player,int playerID)
    {
        // Perform any revive animation or effects here
        yield return new WaitForSeconds(1f); // Example: Wait for 1 seconds for the revive process

        // Reset player position, health, etc.
          if(playerID == 1){
        lastPlayer1Position = respwanPosition;
        newplayer= Instantiate(player, respwanPosition, Quaternion.identity); 
        canvasController.player1=newplayer;//找canvasController腳本
        canvasController.player1Health = newplayer.GetComponent<PlayerHealth>();
        canvasController.cc = newplayer.GetComponent<CharController>();
        targetGroup.m_Targets[0].target = newplayer.transform; // 更新CinemachineTargetGroup中的目標
        shop.ph= newplayer.GetComponent<PlayerHealth>();//找SHOP 腳本
        shop.re1pBtnObj.SetActive(false);
        gamemanger.checkplayer1=newplayer;
        }


        else if (playerID == 2)
        
        { lastPlayer2Position = respwanPosition;
        newplayer= Instantiate(player, respwanPosition, Quaternion.identity); 
        canvasController.player2=newplayer;
        canvasController.player2Health = newplayer.GetComponent<PlayerHealth>();
        canvasController.cc1 = newplayer.GetComponent<CharController1>();
        targetGroup.m_Targets[1].target = newplayer.transform; // 更新CinemachineTargetGroup中的目標
        shop.ph2= newplayer.GetComponent<PlayerHealth>();
        shop.re2pBtnObj.SetActive(false); 
        gamemanger.checkplayer2=newplayer;
        }

       
      


       
       
    }
}
