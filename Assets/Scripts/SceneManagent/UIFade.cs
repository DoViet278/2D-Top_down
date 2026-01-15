using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade> 
{
    [SerializeField] private Image imgFade;
    [SerializeField] private float fadeSpeed = 1.5f;

    private IEnumerator fadeRoutine;

    public void FadeToBlack()
    {
        if(fadeRoutine != null) StopCoroutine(fadeRoutine);

        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);
    }

    public void FadeToClear()
    {
        if(fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);
    }

    private IEnumerator FadeRoutine(float targetAlpha) 
    {
        while (!Mathf.Approximately(imgFade.color.a,targetAlpha))
        {
            float alpha = Mathf.MoveTowards(imgFade.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            imgFade.color = new Color(imgFade.color.r, imgFade.color.g, imgFade.color.b, alpha);
            yield return null;
        }
    }
}
