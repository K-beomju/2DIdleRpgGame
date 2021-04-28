using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private new Renderer renderer;
    public float offset;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void SetSpeed(float speed){
        GameManager.instance.backSpeed = speed;
      speed = GameManager.instance.backSpeed;
    }

    void Update()
    {
        offset += Time.deltaTime * GameManager.instance.backSpeed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset,0));
    }




}
