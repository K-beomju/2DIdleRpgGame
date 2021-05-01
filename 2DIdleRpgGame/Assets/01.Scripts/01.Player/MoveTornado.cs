using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTornado : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      transform.Translate(Vector2.right * 3 * Time.deltaTime);
    }
}
