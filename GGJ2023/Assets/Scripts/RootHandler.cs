using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootHandler : MonoBehaviour
{
    public GameObject playerCamera;
    private bool movingUp;
    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        movingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = transform.position;
        
        if (movingUp)
        {
            newPos.y += 40 * Time.deltaTime;
        }
        else
        {
            newPos.y -= 40 * Time.deltaTime;
        }

        if (movingUp && transform.position.y > -0.64)
        {
            movingUp = false;
            playerCamera.GetComponent<CameraShake>().StartShake();
        }

        transform.position = newPos;

        if (!movingUp && transform.position.y < -10.654)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }

        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
