using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrowing : MonoBehaviour
{
    public GameObject axePrefab;
    public Transform axeSpawnTransform;
    public GameObject heldAxe;
    public LayerMask layerMask;

    private bool hasAxe;
    private Camera mainCamera;
    private Vector3 mousePos;//Not Vector2 as mousePos is used with 'transorm.position' in Update which is Vector3.
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        hasAxe = true;
        heldAxe.SetActive(true);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        //Vector3 rotation = mousePos - transform.position;
        //float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        if (Input.GetMouseButtonDown(1) && hasAxe)
        {
            if (axePrefab)
            {
                Instantiate(axePrefab, axeSpawnTransform.position, Quaternion.identity);
                hasAxe = false;
                heldAxe.SetActive(false);
                animator.SetTrigger("throw");
            }
        }

        if (Input.GetMouseButtonDown(0) && hasAxe)
        {
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
                        enemyHealthComponent.TakeDamage(10);
                    }
                    else
                    {
                        Debug.LogError("Enemy Health Script not found");
                    }
                }
            }
        }
    }

    public void PickUpAxe()
    {
        hasAxe = true;
        heldAxe.SetActive(true);
    }
}
