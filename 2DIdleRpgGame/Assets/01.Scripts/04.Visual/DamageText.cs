using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageText : MonoBehaviour
{


     TextMeshPro text;
      Color alpha;
    public static float damage;



    void Start()
    {

         text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
    }


    void Update()
    {
        if(gameObject.activeSelf)
        {
        transform.Translate(new Vector3(0, 1f * Time.deltaTime, 0)); // 텍스트 위치
             StartCoroutine(SetDeactive());
        }
    }

    private IEnumerator SetDeactive()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }


}
