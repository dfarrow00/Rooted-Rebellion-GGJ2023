using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject branch;
    public GameObject AOEWarning;
    public GameObject player;

    private float attackDelay;
    private float randomAttack;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBranch());
    }

    IEnumerator SpawnBranch()
    {
        yield return new WaitForSecondsRealtime(2);
        float count = 0;

        attackDelay = Random.Range(0.2f, 2f);
        randomAttack = Random.Range(0f, 1f);

        while (true)
        {
            count += 1 * Time.deltaTime;
            if (count > attackDelay)
            {
                count = 0;
                if (randomAttack > 0.8f)
                {
                    Vector2 playerPos = player.transform.position;
                    playerPos.y = -4.63f;
                    Instantiate(AOEWarning, playerPos, Quaternion.Euler(0, 0, 0));
                    attackDelay = Random.Range(0f, 2f);
                }
                else
                {
                    float randomYPosition = Random.Range(-3.15f, 2.94f);
                    Vector2 branchSpawnPos = new Vector2(transform.position.x, randomYPosition);
                    Instantiate(branch, branchSpawnPos, Quaternion.Euler(0, 0, 0));
                    attackDelay = Random.Range(0f, 2f);
                }

                
                randomAttack = Random.Range(0f, 1f);
            }
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
