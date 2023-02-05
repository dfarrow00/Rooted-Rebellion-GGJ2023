using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject branch;
    public GameObject AOEWarning;
    public GameObject player;

    public float minDelay;
    public float maxDelay;

    private Animator animator;

    private float attackDelay;
    private float randomAttack;

    private AudioSource audioSource;
    public AudioClip AOESFX;
    public AudioClip branchSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        StartCoroutine(SpawnBranch());
    }

    IEnumerator SpawnBranch()
    {
        yield return new WaitForSecondsRealtime(2);
        float count = 0;

        attackDelay = Random.Range(minDelay, maxDelay);
        randomAttack = Random.Range(0f, 1f);

        while (true)
        {
            count += 1 * Time.deltaTime;
            if (count > attackDelay)
            {
                count = 0;
                if (randomAttack > 0.8f)
                {
                    SpawnRoot();
                }
                else
                {
                    audioSource.PlayOneShot(branchSound);
                    float randomYPosition = Random.Range(-3.3f, 4.2f);
                    Vector2 branchSpawnPos = new Vector2(transform.position.x, randomYPosition);
                    Instantiate(branch, branchSpawnPos, Quaternion.Euler(0, 0, 90));
                }

                attackDelay = Random.Range(minDelay, maxDelay);
                randomAttack = Random.Range(0f, 1f);
            }
            yield return null;
        }

    }

    public void SpawnRoot()
    {
        audioSource.PlayOneShot(AOESFX);
        animator.Play("AOE Charge");
        Vector2 playerPos = player.transform.position;
        playerPos.y = -5.2f;
        Instantiate(AOEWarning, playerPos, Quaternion.Euler(0, 0, 0));
    }

    public void ChangeMaxDelay(float newDelay)
    {
        maxDelay = newDelay;
    }
}
