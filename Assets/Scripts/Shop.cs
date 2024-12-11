using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Shop : MonoBehaviour

{
    public int hkick_prices =100;

    public GameObject hkickcdtext;
    public bool isBuyhkick=false;

    public bool isBuyedBuyhkick=false;
    public bool buyevent = false;

    public Expupdate eud;

    public Button buyButton;
    // Start is called before the first frame update
    void Start()
    {
        isBuyhkick=false;
        eud=GetComponent<Expupdate>();
        buyButton.onClick.AddListener(Buyhkick); // Add a listener to the button click event
    }

    void Buyhkick()
    {
        if (eud.currentExp >= hkick_prices&&!isBuyedBuyhkick)
        {
            eud.currentExp -= hkick_prices;
            hkickcdtext.SetActive(true);
            isBuyhkick = true;
            isBuyedBuyhkick=true;
            Debug.Log("Buyhkick purchased!");
        }
        else if(isBuyedBuyhkick)
        {
            Debug.Log("You Can not buy again!");
        }
        else
        {
            Debug.Log("Not enough cash!!!");
        }

    }
}
