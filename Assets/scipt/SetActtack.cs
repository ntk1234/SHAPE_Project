using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActtack : MonoBehaviour
{
      public GameObject kickbox,hitbox;
    
    public CharController cc;

    // Start is called before the first frame update
    void Start()
    {
       cc = GetComponent<CharController>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void ActivatePunchHitbox()
    {
        kickbox.SetActive(true);
    }
    public void DeactivatePunchHitbox()
    {
        kickbox.SetActive(false);
        cc.isKick=false;
    }

      public void ActivatePunchHitbox2()
    {
        hitbox.SetActive(true);
    }
    public void DeactivatePunchHitbox2()
    {
        hitbox.SetActive(false);
        cc.isPunch=false;
    }
}
