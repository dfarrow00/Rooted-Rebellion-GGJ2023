using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private float movement;
    private bool grounded = false;
    private bool doubleJumped = false;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        rigidBody.velocity = new Vector2(speed * movement, rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rigidBody.AddForce(new Vector2(0, jumpForce));
                grounded = false;
            }
            else if (!grounded && !doubleJumped)
            {
                rigidBody.AddForce(new Vector2(0, jumpForce));
                doubleJumped = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            doubleJumped = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
