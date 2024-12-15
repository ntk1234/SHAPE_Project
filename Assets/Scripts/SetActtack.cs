using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActtack : MonoBehaviour
{
      public GameObject kickbox,hitbox,hkickbox;
    
    public CharController cc;
    public CharController1 cc1;

    // Start is called before the first frame update
    void Start()
    {
    
        cc = GetComponent<CharController>();
            
        cc1= GetComponent<CharController1>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void ActivateKickHitbox()
    {
        kickbox.SetActive(true);
        
    }
    public void DeactivateKickHitbox()
    {
        kickbox.SetActive(false);
        cc.isKick=false;
    }

     public void ActivateHKickHitbox()
    {
        hkickbox.SetActive(true);
    }
    public void DeactivateHKickHitbox()
    {
        hkickbox.SetActive(false);
        cc.isHKick=false;
    }

      public void ActivatePunchHitbox2()
    {
        hitbox.SetActive(true);
    }
    public void DeactivatePunchHitbox2()
    {
        hitbox.SetActive(false);
        cc.isPunch=false;
        Debug.Log("isPunch=false");
    }


     public void ActivateKickHitbox2P()
    {
        kickbox.SetActive(true);
    }
    public void DeactivateKickHitbox2P()
    {
        kickbox.SetActive(false);
        cc1.isKick=false;
    }

      public void ActivatePunchHitbox2_2P()
    {
        hitbox.SetActive(true);
    }
    public void DeactivatePunchHitbox2_2p()
    {
        hitbox.SetActive(false);
        cc1.isPunch=false;
    }
}
