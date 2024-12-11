using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour

{

    
    public float maxHealth = 100f; // 物件的最大生命值
    public float currentHealth;
    
    public int damage = 10;

    public int exp = 10 ;

    public Expupdate expupdate;
    
    public GameObject gm;

    public GameObject cube;
    
     public GameObject healthPackPrefab;

     public Material originalMaterial;
    public Material flashMaterial;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public float duration = 0.7f; // 閃爍持續時間

    
    // Start is called before the first frame update
    void Start()
    {
         currentHealth = maxHealth;
         expupdate = GameObject.Find("GameManger").GetComponent<Expupdate>();
        skinnedMeshRenderer = cube.GetComponent<SkinnedMeshRenderer>();
        originalMaterial = skinnedMeshRenderer.material;
        originalMaterial.color = Color.white;
        flashMaterial = new Material(Shader.Find("Standard"));
        flashMaterial.color =new Color(0.776f, 0.357f, 0.357f);;

    }

    // Update is called once per frame
    void Update()
    {
            if (currentHealth <= 0)
        {
            if (Random.value < 0.5f) // 50% chance
            {
                expupdate.currentExp += exp;
                Debug.Log("Enemy defeated and gained EXP!");
            }

            Die();
        }
    
    }
  

     public void Die()
    {
          Debug.Log("Enemy died");
      //  Instantiate(healthPackPrefab, transform.position, Quaternion.identity);
        
           if (Random.value < 0.25f)
            {
        DropHealthPack();
            }
        Destroy(gameObject);
    }

    void DropHealthPack()
    {
        // 從預置物體生成血包
        Instantiate(healthPackPrefab, transform.position, Quaternion.identity);
        
         
    }
public void TakeDamage(int damage)
{
    currentHealth -= damage; 
    StartCoroutine(FlashRedAndRecover());

   
}

IEnumerator FlashRedAndRecover()
{
    StartCoroutine(FlashRed());

    yield return new WaitForSeconds(0.5f); // 等待閃爍動畫播放完畢
    
   
    
}

IEnumerator FlashRed()
{
    
    float elapsedTime = 0f;
    Color originalColor = originalMaterial.color;
    Color flashColor = flashMaterial.color;

    while (elapsedTime < duration)
    {
        float t = elapsedTime / duration;
        skinnedMeshRenderer.material.color = Color.Lerp(originalColor, flashColor, Mathf.PingPong(t * 10, 1)); // 使用PingPong函數實現閃爍效果
        elapsedTime += Time.deltaTime;
        yield return null;
    }

     skinnedMeshRenderer.material.color = Color.white;// 將顏色立即變回原本顏色
}



}
