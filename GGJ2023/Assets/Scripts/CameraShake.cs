using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeDuration = 0;
    private float shakeMagnitude = 0.2f;
    private float dampingSpeed = 2.0f;

    Vector3 initialPosition;

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void StartShake()
    {
        shakeDuration = 0.1f;
    }
}
