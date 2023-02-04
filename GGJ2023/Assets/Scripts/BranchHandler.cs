using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchHandler : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private bool move;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        move = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        direction = (player.transform.position - transform.position).normalized;
        
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hello");
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(rigidBody);
            StartCoroutine(DeleteBranch());
        }
    }

    IEnumerator DeleteBranch()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
