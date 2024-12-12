using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // 物件的最大生命值
    public float currentHealth;
    public int damage = 10;
    public int exp = 10;
    public Expupdate expupdate;
    public GameObject gm;
    
    public GameObject healthPackPrefab;
    public List<GameObject> otherPackPrefabs = new List<GameObject>();
    public Material originalMaterial;

    public Material originalMaterial2;
    public Material flashMaterial;
    public float duration = 0.7f; // 閃爍持續時間
    public List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

     public List<GameObject> cubes = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        expupdate = GameObject.Find("GameManger").GetComponent<Expupdate>();

        foreach (GameObject cube in cubes)
        {
            SkinnedMeshRenderer[] renderers = cube.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer renderer in renderers)
            {
                skinnedMeshRenderers.Add(renderer);
                originalMaterial = renderer.material;
                originalMaterial.color = renderer.material.color;
               
            }
        }

        flashMaterial = new Material(Shader.Find("Standard"));
        flashMaterial.color = new Color(0.776f, 0.357f, 0.357f);
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

        if (Random.value < 0.25f)
        {
            DropHealthPack();
        }
        if (otherPackPrefabs.Count!=0){
            DropOtherPack();
        }      
        Destroy(gameObject);
    }

    void DropHealthPack()
    {

        Instantiate( healthPackPrefab, transform.position, Quaternion.identity);
    }
     void DropOtherPack()
    {
        // 從預置物體生成血包
        int randomIndex = Random.Range(0, otherPackPrefabs.Count);
        GameObject selectedOtherPackPrefab = otherPackPrefabs[randomIndex];

        Instantiate(selectedOtherPackPrefab, transform.position, Quaternion.identity);
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
            foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
            {
                renderer.material.color = Color.Lerp(originalColor, flashColor, Mathf.PingPong(t * 10, 1));
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
        {
            renderer.material.color = Color.white; // 將顏色立即變回原本顏色

        //renderer.material.color = originalColor ; 
        }
    }
}