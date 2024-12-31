using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;


public class Shop : MonoBehaviour

{

    public int respawn1p_prices =10;

    public int respawn2p_prices =10;

    public int hkick_prices =100;

    public int smallheal_prices = 20 ;

    public int mg_prices =100;
    public GameObject hkickcdtext,mgtext;
    public bool isBuyhkick=false;

    public bool isBuymg=false;
    public bool isBuy1Pre = false;

    public bool isBuy2Pre = false;
    public GameObject gm,re1pBtnObj,re2pBtnObj,shopTextObj;

  

    public PlayerHealth ph,ph2;

    public Expupdate eud;

    public ReviveHealthManger rhm;

    public Button buyhkButton,buySmallBtn,buymgBtn,re1pBtn,re2pBtn;


    public TextMeshProUGUI shopText; 
    public TextMeshProUGUI shop1pskillText,shop2pskillText,shopHealText,shopRe1Text,shopRe2Text; 
    // Start is called before the first frame update
    void Start()
    {
        isBuyhkick=false;
        isBuymg=false;

        rhm=gm.GetComponent<ReviveHealthManger>();

        eud=GetComponent<Expupdate>();
        ph=GameObject.Find("1PlayerArmature").GetComponent<PlayerHealth>();
        ph2=GameObject.Find("2PlayerArmature").GetComponent<PlayerHealth>();
        buyhkButton.onClick.AddListener(Buyhkick); // Add a listener to the button click event
        buySmallBtn.onClick.AddListener(BuySmallHeal);
        buymgBtn.onClick.AddListener(Buymg);

        re1pBtn.onClick.AddListener(Buyre1p);
        re2pBtn.onClick.AddListener(Buyre2p);
        //re2pBtn.onClick.AddListener();

        re1pBtnObj.SetActive(false);
        re2pBtnObj.SetActive(false);
        shopText=shopTextObj.GetComponent<TextMeshProUGUI>();
        shopText.text=" ";
        shop1pskillText.text="$ "+hkick_prices;
        shop2pskillText.text="$ "+mg_prices;
        shopHealText.text="$ "+smallheal_prices;
        shopRe1Text.text="$ "+respawn1p_prices;
        shopRe2Text.text="$ "+respawn2p_prices;
    }

    void Buyhkick()
    {
        if (eud.currentExp >= hkick_prices&&!isBuyhkick )
        {
            eud.currentExp -= hkick_prices;
            hkickcdtext.SetActive(true);
            isBuyhkick = true;
           // isBuyedBuyhkick=true;
            Debug.Log("Buyhkick purchased!");

            shopText.text="Buy 1p Skill purchased!";
        }
        else if(isBuyhkick)
        {
            Debug.Log("You Can not buy again!");
             shopText.text="You can not buy again!";
        }
        else
        {
            Debug.Log("Not enough cash!!!");
            shopText.text="Not enough cash!!!";
        }

    }
      void Buymg()
    {
        if (eud.currentExp >= mg_prices&&!isBuymg)
        {
            eud.currentExp -= mg_prices;
            mgtext.SetActive(true);
            isBuymg = true;
           // isBuyedBuyhkick=true;
            Debug.Log("Buymg purchased!");
            shopText.text="Buy 2p Skill purchased!";
        }
        else if(isBuymg)
        {
            Debug.Log("You Can not buy again!");
            shopText.text="You can not buy again!";
        }
        else
        {
            Debug.Log("Not enough cash!!!");
            shopText.text="Not enough cash!!!";
        }

    }

     void BuySmallHeal()
    {
        if (eud.currentExp >= smallheal_prices)
        {
            if(ph.currentHealth!=ph.maxHealth||ph2.currentHealth!=ph2.maxHealth)
            
            {eud.currentExp -= smallheal_prices;

            ph.Heal(20);    
            ph2.Heal(20); 
            Debug.Log("Buyheal purchased!");
            shopText.text=$"1p Player healed. HP: {ph.currentHealth}/100\n"+$"2p Player healed. HP: {ph2.currentHealth}/100";
            if(ph==null)
            {
             shopText.text=$"1p Player died. HP: 0/100\n"+$"2p Player healed. HP: {ph2.currentHealth}/100";
            }
             if(ph2==null)
            {
                shopText.text=$"1p Player healed. HP: {ph.currentHealth}/100\n"+$"2p Player died. HP: 0/100";
            }
            if(isBuy1Pre)
            {
            shopText.text=$"1p Player resurrected. HP: 100/100\n"+$"2p Player healed. HP: {ph2.currentHealth}/100";
            }
            if(isBuy2Pre)
            {
            shopText.text=$"1p Player healed. HP: {ph.currentHealth}/100\n"+$"2p Player resurrected. HP: 100/100";
            }
            }
            
            else
            {
                 Debug.Log("You are full hp!");
                shopText.text="You are full hp!";
            }
            
        }
        else
        {
            Debug.Log("Not enough cash!!!");
            shopText.text="Not enough cash!!!";
        }

    }

    
     void Buyre1p() //buy re1 method
    {
        if (eud.currentExp >=  respawn1p_prices&&!isBuy1Pre)
        {
            
            
            eud.currentExp -= respawn1p_prices;


        // Call the RevivePlayer method with the delegate
            rhm.RevivePlayer1P();
;
            isBuy1Pre=true;
    
            Debug.Log("Buyre1p purchased!");
            shopText.text="1p player resurrected!";
         
        }
        else if(isBuy1Pre)
        {
            Debug.Log("You Can not buy re1 again!");
            shopText.text="You can not buy again!";
        }
        else
        {
            Debug.Log("Not enough cash!!!");
             shopText.text="Not enough cash!!!";
        }

    }

     void Buyre2p()
    {
        if (eud.currentExp >=  respawn2p_prices&&!isBuy2Pre)
        {
            
            
            eud.currentExp -= respawn2p_prices;


        // Call the RevivePlayer method with the delegate
            rhm.RevivePlayer2P();

          
            isBuy2Pre=true;

            Debug.Log("Buyre2p purchased!");
            shopText.text="2p player resurrected!";
         
        }
         else if(isBuy2Pre)
        {
            Debug.Log("You Can not buy re2 again!");
             shopText.text="You can not buy again!";
        }
        else
        {
            Debug.Log("Not enough cash!!!");
             shopText.text="Not enough cash!!!";
        }

    }

}
