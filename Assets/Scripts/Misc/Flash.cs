using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private float flashDuration = 0.1f;

    private SpriteRenderer sr;
    private Material originalMat;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMat = sr.material;    
    }
    
    public void FlashSprite()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        sr.material = mat;
        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMat;
    }

}
