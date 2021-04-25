using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private new Renderer renderer;

    public float speed = 0.5f;
    public float offset;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }

    void Update()
    {
        offset += Time.deltaTime * speed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset,0));
    }


}
