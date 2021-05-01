using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public float reduceFactor = 4f;
    private Slider slider;
    private RectTransform rTr;

    private Coroutine co = null;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        rTr = GetComponent<RectTransform>();
    }

    public void SetValue(float value)
    {
        if (co != null) StopCoroutine(co);
        co = StartCoroutine(DamageReduce(value));
    }

     private void OnDisable()
    {
         StopAllCoroutines();
    }



    IEnumerator DamageReduce(float value)
    {
        while (true)
        {
            slider.value = value; //Mathf.Lerp(slider.value, value,  reduceFactor);
            if (Mathf.Abs(slider.value - value) < 0.1f)
                yield break; // 종료
            yield return null; // null 반환
        }
    }


    public void Reset(Vector3 pos, float value) // 슬라이더 위치 리셋
    {
        rTr.position = pos;
        slider.value = value;
    }


    public void SetPosition(Vector3 pos) // 슬라이더 포지션
    {
        rTr.position = pos;
    }
}
