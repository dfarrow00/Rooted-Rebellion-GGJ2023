using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePickup : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(new Vector2(-300, 200));
        rigidBody.AddTorque(50);
    }

    //If player collides, notify AxeCombat script.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AxeCombat axeCombatScript = collision.gameObject.GetComponentInChildren<AxeCombat>();
            if (axeCombatScript)
            {
                axeCombatScript.PickUpAxe();
            }
            Destroy(gameObject);
        }
    }
}
