using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItem
{
    public GameObject prefab;
    [Range(0f, 1f)]
    public float dropChance;
}

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f; // Maximum health of the enemy
    public float currentHealth;

    [Header("Damage and EXP")]
    public int damage = 10;
    public int exp = 10;
    public Expupdate expUpdate;

    [Header("Loot Settings")]
    public List<LootItem> lootObjects = new List<LootItem>();

    [Header("Visual Effects")]
    public Material originalMaterial;
    public Material flashMaterial;
    public float flashDuration = 0.7f; // Duration of the flash effect
    public List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

    [Header("Optional Settings")]
    public List<GameObject> cubes = new List<GameObject>();

    private void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Find Expupdate component
        expUpdate = GameObject.Find("GameManger")?.GetComponent<Expupdate>();

        // Gather SkinnedMeshRenderers and setup materials
        foreach (GameObject cube in cubes)
        {
            SkinnedMeshRenderer[] renderers = cube.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer renderer in renderers)
            {
                skinnedMeshRenderers.Add(renderer);
                originalMaterial = renderer.material;
            }
        }

        // Create flash material
        flashMaterial = new Material(Shader.Find("Standard"))
        {
            color = new Color(0.776f, 0.357f, 0.357f)
        };
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        StartCoroutine(FlashEffect());
    }

    private void HandleDeath()
    {
        Debug.Log("Enemy defeated and gained EXP!");

        // Grant EXP
        if (Random.value < 0.5f)
        {
            expUpdate.currentExp += exp;
        }

        // Drop loot
        DropLoot();

        Destroy(gameObject);
    }

    private void DropLoot()
    {
        foreach (var lootItem in lootObjects)
        {
            if (Random.value < lootItem.dropChance)
            {
                Instantiate(lootItem.prefab, transform.position, Quaternion.identity);
            }
        }
    }

    private IEnumerator FlashEffect()
    {
        float elapsedTime = 0f;
        Color originalColor = originalMaterial.color;
        Color flashColor = flashMaterial.color;

        while (elapsedTime < flashDuration)
        {
            float lerpValue = Mathf.PingPong(elapsedTime * 10, 1);
            foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
            {
                renderer.material.color = Color.Lerp(originalColor, flashColor, lerpValue);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
        {
            renderer.material.color = originalColor;
        }
    }
}
