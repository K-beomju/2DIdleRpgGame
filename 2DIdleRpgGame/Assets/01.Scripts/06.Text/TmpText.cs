using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TmpText : MonoBehaviour
{

   public float moveSpeed;
    public float alphaSpeed;
    public float destroyTime;
    TextMeshPro text;
    Color alpha;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = GameManager.instance.attackDamage.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }



}
