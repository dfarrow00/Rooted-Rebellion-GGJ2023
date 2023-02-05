using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCombat : MonoBehaviour
{
    public GameObject axePrefab;
    public Transform axeSpawnTransform;
    public GameObject heldAxe;
    public LayerMask layerMask;
    public GameObject arrowRotation;
    public float meleeDelay;

    private bool hasAxe;
    private bool canMelee;
    private Camera mainCamera;
    private Vector3 mousePos;
    private Animator animator;

    private AudioSource audioSource;
    public AudioClip attackSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        hasAxe = true;
        canMelee = true;
        heldAxe.SetActive(true);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //Calculates the angle between the arrow rotation point and the mouse to rotate the aiming arrow.
        Vector3 rotation = mousePos - arrowRotation.transform.position;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        arrowRotation.transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        //When throw button pressed, instantiates axe projectile and hides held axe.
        if (Input.GetButtonDown("Fire2") && hasAxe)
        {
            if (axePrefab)
            {
                audioSource.PlayOneShot(attackSound);

                Instantiate(axePrefab, axeSpawnTransform.position, Quaternion.identity);
                hasAxe = false;
                heldAxe.SetActive(false);
                animator.SetTrigger("throw");
            }
        }

        //When melee pressed, LineCast is used to detect if enemy is in range. Applies damage if is.
        if (Input.GetButtonDown("Fire1") && hasAxe && canMelee)
        {
            audioSource.PlayOneShot(attackSound);

            canMelee = false;
            Invoke(nameof(MeleeDelay), meleeDelay);
            animator.SetTrigger("throw");
            RaycastHit2D hitResult = Physics2D.Linecast(transform.position + new Vector3(0, 1), transform.position + new Vector3(mousePos.x > transform.position.x ? 1.5f : -1.5f, 1), layerMask);
            Debug.DrawLine(transform.position + new Vector3(0, 1), transform.position + new Vector3(mousePos.x > transform.position.x ? 1.5f : -1.5f, 1), Color.red, 1f);
            if (hitResult.collider != null)
            {
                if (hitResult.collider.gameObject.tag == "Enemy")
                {
                    EnemyHealth enemyHealthComponent = hitResult.collider.gameObject.GetComponent<EnemyHealth>();
                    if (enemyHealthComponent)
                    {
                        enemyHealthComponent.TakeDamage(5, 1);
                    }
                    else
                    {
                        Debug.LogError("Enemy Health Script not found");
                    }
                }
            }
        }
    }

    private void MeleeDelay()
    {
        canMelee = true;
    }

    //Displays held axe and allows player to melee and throw again.
    public void PickUpAxe()
    {
        hasAxe = true;
        heldAxe.SetActive(true);
    }
}
