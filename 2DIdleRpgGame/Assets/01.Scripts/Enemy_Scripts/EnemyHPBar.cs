using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public float reduceFactor = 4f; //4배로 줄어들기
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
            slider.value = Mathf.Lerp(slider.value, value, Time.deltaTime * reduceFactor);
            if (Mathf.Abs(slider.value - value) < 0.1f)
                yield break;
            yield return null; //프레임단위 실행
        }
    }

    //초기 HP바를 셋팅함
    public void Reset(Vector3 pos, float value)
    {
        rTr.position = pos;
        slider.value = value;
    }

    //HP바의 위치를 셋팅함
    public void SetPosition(Vector3 pos)
    {
        rTr.position = pos;
    }
}
