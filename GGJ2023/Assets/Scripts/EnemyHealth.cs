using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float health;

    public Scrollbar healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.size = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.size = health / maxHealth;
    }
}
