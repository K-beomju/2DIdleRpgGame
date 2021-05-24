using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private  Renderer renderers;
    public float offset;
    private float speed;
    void Start()
    {
        renderers = GetComponent<Renderer>();
    }

   public void SetSpeed(float speed){
        this.speed = speed;
    }

    void Update()
    {
        offset += Time.deltaTime * speed;
        renderers.material.SetTextureOffset("_MainTex", new Vector2(offset,0));
    }




}
