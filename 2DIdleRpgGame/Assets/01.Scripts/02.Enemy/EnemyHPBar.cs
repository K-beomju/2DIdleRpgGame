using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public float reduceFactor = 4f; // 부드러운 데미지 감소 ?
    private Slider slider; // 슬라이더
    private RectTransform rTr; // 렉트위치

    private Coroutine co = null; // 코루틴 변수 선언
    private void Awake()
    {
        slider = GetComponent<Slider>();
        rTr = GetComponent<RectTransform>();
    }

    public void SetValue(float value)
    {
        if (co != null) StopCoroutine(co);  // co가 실행중이라면 =  현재 어떤 코루틴 함수가 실행 중이라는 뜻
        co = StartCoroutine(DamageReduce(value)); // DamageReduce 코루틴 시작
    }

     private void OnDisable() //비활성화가 될 때 한번 실행
    {
         StopAllCoroutines(); // 전체 코루틴 스탑
    }



    IEnumerator DamageReduce(float value) //매개 변수 데미지
    {
        while (true) // 루프
        {
            slider.value = Mathf.Lerp(slider.value, value, Time.deltaTime * reduceFactor); // 감소
            if (Mathf.Abs(slider.value - value) < 0.1f) // 슬라이더 벨루가 0.1 미만이면
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
