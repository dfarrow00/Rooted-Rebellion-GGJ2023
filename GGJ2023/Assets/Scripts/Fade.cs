using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameObject root;

    private SpriteRenderer sprite;
    private float alpha;
    private bool reverse;
    void Start()
    {
        reverse = false;
        alpha = 0;
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(AOEWarning());
    }

    IEnumerator AOEWarning()
    {
        double timer = 0;
        while (true)
        {
            timer += 1 * Time.deltaTime;

            if (timer > 0.03)
            {
                if (!reverse)
                {
                    alpha = Mathf.Lerp(alpha, 1, 0.1f);
                }
                else
                {
                    alpha = Mathf.Lerp(alpha, 0, 0.3f);
                }

                Color newColor = new Color(1, 0, 0, alpha);
                sprite.color = newColor;
                timer = 0;
            }

            if (alpha > 0.75f && !reverse)
            {
                yield return new WaitForSecondsRealtime(0.25f);
                reverse = true;

                Vector2 rootSpawn = transform.position;
                rootSpawn.y = -10.654f;
                Instantiate(root, rootSpawn, Quaternion.Euler(0,0,0));
            }

            else if (alpha < 0.1f && reverse)
            {
                Destroy(gameObject);
                break;
            }

            yield return null;
        }
    }
}
