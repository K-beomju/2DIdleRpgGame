using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roulette : MonoBehaviour
{
    public float rotateSpeed = 0f;
    int count;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            count++;
            if(count == 1)
            rotateSpeed = 10;
            else
             this.rotateSpeed *= 0.99f;

        }
            transform.Rotate(0,0,rotateSpeed);
        this.rotateSpeed *= 0.99f; //소수 구해서 함수 리턴 배열에
    }
}
