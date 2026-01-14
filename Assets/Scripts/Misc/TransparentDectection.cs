using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDectection : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float tranparencyAmount = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer sr;
    private Tilemap tilemap;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (sr)
            {
                StartCoroutine(FadeRoutine(sr, fadeTime, sr.color.a, tranparencyAmount));
            }
            else if (tilemap)
            {
                StartCoroutine(FadeTileMapRoutine(tilemap, fadeTime, tilemap.color.a, tranparencyAmount));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (sr)
            {
                StartCoroutine(FadeRoutine(sr, fadeTime, sr.color.a, 1f));
            }
            else if (tilemap)
            {
                StartCoroutine(FadeTileMapRoutine(tilemap, fadeTime, tilemap.color.a, 1f));
            }
        }
    }


    private IEnumerator FadeRoutine(SpriteRenderer sr, float fadeTime, float startValue, float targetTranspancy)
    {
        float elapsedTime = 0;
        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTranspancy, (elapsedTime / fadeTime));
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeTileMapRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTranspancy)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime+= Time.deltaTime;   
            float newAlpha = Mathf.Lerp(startValue, targetTranspancy, (elapsedTime / fadeTime));
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha); 
            yield return null;
        }
    }
}
