using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectileMovement : MonoBehaviour
{
    public float speed;
    public GameObject axePickup;

    private Vector3 mousePos;
    private Camera mainCamera;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidBody = GetComponent<Rigidbody2D>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rigidBody.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        rigidBody.AddTorque(direction.x < 0 ? 1 : -1, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 15 || transform.position.x < -15)
        {
            Instantiate(axePickup, new Vector2(8, 5), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealthComp = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealthComp)
            {
                enemyHealthComp.TakeDamage(10);
            }
            transform.parent = collision.gameObject.transform;
            Destroy(rigidBody);
            StartCoroutine(SpawnAxePickup());
        }
    }

    private IEnumerator SpawnAxePickup()
    {
        yield return new WaitForSeconds(2.0f);
        if (axePickup)
        {
            Instantiate(axePickup, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Axe Pickup object not assigned");
        }
        Destroy(gameObject);
    }
}
