using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public Material hitEffectMaterial;
    public float hitEffectDuration;
    public Scrollbar healthBar;

    private float health;
    private Material originalMaterial;
    private SpriteRenderer spriteRenderer;
    private Coroutine hitEffectRoutine;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.size = health / maxHealth;
        if (health <= 0)
        {
            Debug.Log("Player Died");
        }
        if (hitEffectRoutine != null)
        {
            StopCoroutine(PlayHitEffect());
        }
        hitEffectRoutine = StartCoroutine(PlayHitEffect());
    }

    private IEnumerator PlayHitEffect()
    {
        spriteRenderer.material = hitEffectMaterial;

        yield return new WaitForSeconds(hitEffectDuration);

        spriteRenderer.material = originalMaterial;

        hitEffectRoutine = null;
    }
}
