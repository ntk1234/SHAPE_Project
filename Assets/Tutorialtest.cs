using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorialtest: MonoBehaviour
{
    public string showtext;

    public Text tul_text;

    public Button nextbtn,backbtn;

    public GameObject nextbtnobj,backbtnobj;


    // Start is called before the first frame update
    void Start()
    {
        nextbtnobj.SetActive(true);
        backbtnobj.SetActive(false);
        Showtul1();
        tul_text.text=showtext.ToString();
        nextbtn.onClick.AddListener(callshowtul2);
        backbtn.onClick.AddListener(callshowtul1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Showtul1()
    {
       showtext= "1. Movement\n"+
        "1P Movement: Use the WASD keys.\n"+
        "2P Movement: Use the Arrow keys.\n"+
        "\n"+
        "2. Attack & Magic Points (MP)\n"+
        "1P Attack: Press E to attack and increase MP.\n"+
        "2P Attack: Press B to attack and increase MP.";
    }

    void Showtul2()
    {
        showtext="3. Special Attack\n"+
                " When the corresponding icon lights up, you can launch\n"+
                "a special attack.\n"+
                "\n"+
                "4. Earning Coines\n"+
                " Earn coines by defeating enemies.\n"+
                "\n"+
                "5. Upgrades\n"+
                "At the end of Wave, use the coins you've earned to\n"+
                 "buy items and upgrade your character.";
    }

     public void callshowtul1()
    {
        Showtul1();
        tul_text.text=showtext.ToString();
        nextbtnobj.SetActive(true);
        backbtnobj.SetActive(false);
    }

    public void callshowtul2()
    {
        Showtul2();
        tul_text.text=showtext.ToString();
        nextbtnobj.SetActive(false);
        backbtnobj.SetActive(true);
    }
}
