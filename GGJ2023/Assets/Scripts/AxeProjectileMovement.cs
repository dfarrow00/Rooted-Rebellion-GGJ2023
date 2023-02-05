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
    
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidBody = GetComponent<Rigidbody2D>();

        //Direction of mouse relative to the player is calculated and the object is thrown in that direction.
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rigidBody.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        //Rotates the axe depending on which way the player is facing.
        rigidBody.AddTorque(direction.x < 0 ? 1 : -1, ForceMode2D.Impulse);
    }

    void Update()
    {
        //If axe goes out of level bounds, a new axe pickup will spawn in the level.
        if (transform.position.x > 15 || transform.position.x < -15)
        {
            Instantiate(axePickup, new Vector2(8, 5), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If enemy is hit, stick to them and stop moving.
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealthComp = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealthComp)
            {
                enemyHealthComp.TakeDamage(10, 2);
            }
            transform.parent = collision.gameObject.transform;
            Destroy(rigidBody);
            StartCoroutine(SpawnAxePickup(2.0f));
        }
        else
        {
            StartCoroutine(SpawnAxePickup(1.0f));
        }
    }

    //After a delay, spawn a new axe pickup and destory self.
    private IEnumerator SpawnAxePickup(float delay)
    {
        yield return new WaitForSeconds(delay);
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
