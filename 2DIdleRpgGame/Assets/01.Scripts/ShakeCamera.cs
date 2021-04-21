using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float ShakeAmount;
    float ShakeTime;
    Vector3 initialPosition;
    public void VibrateForTime(float time)
    {
        ShakeTime = time;
    }

    private void Update()
    {
        initialPosition = GameObject.FindWithTag("MainCamera").transform.position;//카메라 흔들릴 위치값
        if (ShakeTime > 0)
        {
            transform.position = Random.insideUnitSphere * ShakeAmount + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            ShakeTime = 0.0f;
            transform.position = initialPosition;
        }
    }
}
