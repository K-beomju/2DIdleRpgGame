using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private new Renderer renderer;
    public static float speed = 1f;
    public float offset;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        offset += Time.deltaTime * speed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset,0));
    }
}
