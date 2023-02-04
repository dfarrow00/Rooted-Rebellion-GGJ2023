using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePickup : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(new Vector2(-300, 200));
        rigidBody.AddTorque(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AxeThrowing axeThrowingScript = collision.gameObject.GetComponentInChildren<AxeThrowing>();
            if (axeThrowingScript)
            {
                axeThrowingScript.PickUpAxe();
            }
            Destroy(gameObject);
        }
    }
}
