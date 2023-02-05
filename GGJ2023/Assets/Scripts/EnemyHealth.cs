using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    private float health;

    public Scrollbar healthBar;
    private EnemyAI enemyAI;

    private GameController gameController;
    private Animator animator;

    private bool isDead;
    void Start()
    {
        animator = GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        enemyAI = GetComponent<EnemyAI>();

        health = maxHealth;
        healthBar.size = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount, int type)
    {
        if (!isDead)
        {
            if (type == 1)
            {
                float rand = Random.Range(0f, 1f);
                if (rand > 0.75)
                {
                    enemyAI.SpawnRoot();
                }

            }
            health -= amount;
            healthBar.size = health / maxHealth;
            if (health <= 0)
            {
                isDead = true;
                enemyAI.StopAllCoroutines();
                animator.Play("Tree Death Anim");
                gameController.PlayerWin();
            }
            else if (health / maxHealth <= 0.25f)
            {
                enemyAI.ChangeMaxDelay(1.25f);
            }
            else if (health / maxHealth <= 0.5f)
            {
                enemyAI.ChangeMaxDelay(1.75f);
            }
            else if (health / maxHealth <= 0.75f)
            {
                enemyAI.ChangeMaxDelay(2.25f);
            }

            animator.Play("Tree Take Hit");
        }
        
    }
}
