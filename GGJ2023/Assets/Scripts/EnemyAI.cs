using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public BranchHandler branch;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBranch());
    }

    IEnumerator SpawnBranch()
    {
        float count = 0;
        while (true)
        {
            count += 1 * Time.deltaTime;
            if (count > 2)
            {
                count = 0;
                Instantiate(branch, transform.position, Quaternion.Euler(0, 0, 0));
            }
            yield return null;
            /*yield return new WaitForSeconds(2);*/
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
