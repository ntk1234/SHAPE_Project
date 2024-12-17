using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Shop : MonoBehaviour

{
    public int hkick_prices =100;

    public int smallheal_prices = 20 ;

    public int mg_prices =100;
    public GameObject hkickcdtext,mgtext;
    public bool isBuyhkick=false;

    //public bool isBuyedBuyhkick=false;
   // public bool buyevent = false;

    public bool isBuymg=false;

    public PlayerHealth ph,ph2;

    public Expupdate eud;

    public Button buyhkButton,buySmallBtn,buymgBtn;
    // Start is called before the first frame update
    void Start()
    {
        isBuyhkick=false;
        isBuymg=false;
        eud=GetComponent<Expupdate>();
        ph=GameObject.Find("1PlayerArmature").GetComponent<PlayerHealth>();
        ph2=GameObject.Find("2PlayerArmature").GetComponent<PlayerHealth>();
        buyhkButton.onClick.AddListener(Buyhkick); // Add a listener to the button click event
        buySmallBtn.onClick.AddListener(BuySmallHeal);
        buymgBtn.onClick.AddListener(Buymg);
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
        }
        else if(isBuyhkick)
        {
            Debug.Log("You Can not buy again!");
        }
        else
        {
            Debug.Log("Not enough cash!!!");
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
        }
        else if(isBuymg)
        {
            Debug.Log("You Can not buy again!");
        }
        else
        {
            Debug.Log("Not enough cash!!!");
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
            Debug.Log("Buyheal purchased!");}
            else
            {
                 Debug.Log("You are full hp!");
            }
        }
        else
        {
            Debug.Log("Not enough cash!!!");
        }

    }
}
