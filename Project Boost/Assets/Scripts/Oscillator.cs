using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] [Range(1f, 10f)]float period = 10f;
    const float tau = Mathf.PI * 2;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period; //Hz

        float rawSinWave = Mathf.Sin(cycles * tau);//2pi*tau is equal to radians

        movementFactor = (rawSinWave + 1f) / 2f;//Sin gives range between -1 to 1. So it is clamped to 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
