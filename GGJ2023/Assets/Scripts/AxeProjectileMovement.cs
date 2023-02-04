using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectileMovement : MonoBehaviour
{
    public float speed;

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

        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
