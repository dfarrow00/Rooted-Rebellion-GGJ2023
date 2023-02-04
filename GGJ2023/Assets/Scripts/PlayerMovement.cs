using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public TrailRenderer trailRenderer;
    public float dashingForce;
    public float dashingDuration;
    public float dashingCooldown;

    private float movement;
    private bool grounded = false;
    private bool doubleJumped = false;
    private bool canDash = true;
    private bool isDashing;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        movement = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                grounded = false;
            }
            else if (!grounded && !doubleJumped)
            {
                rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doubleJumped = true;
                StartCoroutine(EmitTrail(0.1f));
            }
        }

        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rigidBody.velocity = new Vector2(speed * movement, rigidBody.velocity.y);
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float normalGravity = rigidBody.gravityScale;
        rigidBody.gravityScale = 0;

        rigidBody.velocity = new Vector2((movement < 0 ? -1 : 1) * dashingForce, 0);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashingDuration);

        trailRenderer.emitting = false;
        rigidBody.gravityScale = normalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    private IEnumerator EmitTrail(float time)
    {
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(time);
        trailRenderer.emitting = false;
    }
}
