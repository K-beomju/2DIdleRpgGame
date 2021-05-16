using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Text;

public enum eFadeState
{
    None,
    FadeOut,
    ChangeBg,
    FadeIn,
    Done
}


public class FadeCo : MonoBehaviour
{

    [SerializeField]
    float fadeOutCount;
    eFadeState fadeState;
    float timer;

    Image imgBg;

    IEnumerator iStateCo = null;

    StringBuilder sb;

    public SpawnManager spawn;
    public GameObject hPBar;

    private void Awake()
    {
        imgBg = this.gameObject.GetComponent<Image>();
        if (imgBg == null)
        {
            Debug.Log("img is null");
        }
        sb = new StringBuilder();
    }
    void Update()
    {
        if (GameManager.instance.isFadeInOut && fadeState == eFadeState.None)
        {
            hPBar.SetActive(false);
            spawn.enabled = false;
            fadeState = eFadeState.None;
            NextState();
        }
    }

    IEnumerator NoneState()
    {
        while (fadeState == eFadeState.None)
        {
            fadeState = eFadeState.FadeOut;
            yield return null;
        }

        NextState();
    }

    IEnumerator FadeOutState()
    {
        float alpha = 0;



        while (fadeState == eFadeState.FadeOut)
        {
            timer += Time.deltaTime;
            if (alpha < 1)
            {


                alpha += Time.deltaTime;


            }
            else
            {
                if (timer > fadeOutCount)
                {
                    fadeState = eFadeState.ChangeBg;
                    GameManager.instance.isFadeInOut = false;
                    spawn.enabled = true;
                   hPBar.SetActive(true);
                    timer = 0;

                }
            }

            alpha = Mathf.Clamp(alpha, 0, 1);
            imgBg.color = new Color(imgBg.color.r, imgBg.color.g, imgBg.color.b, alpha);
            yield return null;
        }
        NextState();
    }

    IEnumerator ChangeBgState()
    {
        yield return null;

        Debug.Log("리소스 로드 단계");

        fadeState = eFadeState.FadeIn;

        NextState();
    }


    IEnumerator FadeInState()
    {
        float alpha = 1;

        while (fadeState == eFadeState.FadeIn)
        {
            if (alpha > 0)
            {
                alpha -= Time.deltaTime;
            }
            else
            {
                fadeState = eFadeState.Done;
            }

            alpha = Mathf.Clamp(alpha, 0, 1);
            imgBg.color = new Color(imgBg.color.r, imgBg.color.g, imgBg.color.b, alpha);
            yield return null;
        }

        NextState();
    }

    IEnumerator DoneState()
    {
        yield return null;

        fadeState = eFadeState.None;

        Debug.Log("마무리 처리");
    }


    void NextState()
    {
        sb.Remove(0, sb.Length);
        sb.Append(fadeState.ToString());
        sb.Append("State");

        MethodInfo mInfo = this.GetType().GetMethod(sb.ToString(), BindingFlags.Instance | BindingFlags.NonPublic);
        iStateCo = (IEnumerator)mInfo.Invoke(this, null);
        StartCoroutine(iStateCo);
    }
}
