using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float duration = 0;
    private float impact = 0.2f;
    private float fallof = 2.0f;

    Vector3 initialPosition;

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }
    void Update()
    {
        if (duration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * impact;

            duration -= Time.deltaTime * fallof;
        }
        else
        {
            duration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void StartShake()
    {
        duration = 0.1f;
    }
}
