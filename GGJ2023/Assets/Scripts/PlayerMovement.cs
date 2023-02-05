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

    private Camera mainCamera;
    private float minXBounds;
    private float maxXBounds;
    private float playerWidth;
    private float movement;
    private bool grounded = false;
    private bool doubleJumped = false;
    private bool canDash = true;
    private bool isDashing;
    private bool flipped= false;
    private Animator animator;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        animator = GetComponent<Animator>();

        playerWidth = GetComponent<BoxCollider2D>().bounds.size.x;

        minXBounds = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + (playerWidth / 2);
        maxXBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        movement = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(movement));

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

        animator.SetBool("isInAir", !grounded);

        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }

        if (gameObject.transform.position.x < minXBounds)
        {
            gameObject.transform.position = new Vector2(minXBounds, transform.position.y);
        }
        else if (gameObject.transform.position.x > maxXBounds)
        {
            gameObject.transform.position = new Vector2(maxXBounds, transform.position.y);
        }

        if (mainCamera.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            flipped = true;
        }
        else
        {
            flipped = false;
        }
        transform.rotation = Quaternion.Euler(0f, flipped ? 180f : 0, 0f);
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
