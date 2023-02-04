using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchHandler : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(direction * speed, ForceMode2D.Impulse);
        //pls work
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(rigidBody);
            StartCoroutine(DeleteBranch());
        }
        else if (collision.gameObject.tag == "Player")
        {
            PlayerHealth playerHealthComp = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealthComp)
            {
                playerHealthComp.TakeDamage(5);
            }
            Destroy(gameObject);
        }

        IEnumerator DeleteBranch()
        {
            yield return new WaitForSecondsRealtime(2);
            gameObject.SetActive(false);
        }
    }
}
