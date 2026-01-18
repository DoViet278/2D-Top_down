using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float laserGrowTime = 0.22f;
    private float laserRange;
    private SpriteRenderer sr;
    private CapsuleCollider2D capsuleCollider;
    private SpriteFade spriteFade;
    private bool isGrowing = true;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteFade = GetComponent<SpriteFade>();    
    }

    private void Start()
    {
        FaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.GetComponent<Indestructible>())
        {
            isGrowing = false;
        }
    }

    public void SetLaserRange(float range)
    {
       this.laserRange = range;
        StartCoroutine(IncreaseLaserLength());
    }

    private IEnumerator IncreaseLaserLength()
    {
        float timePassed = 0f;

        while(sr.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime; 
            sr.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);
            capsuleCollider.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), capsuleCollider.size.y);
            capsuleCollider.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT))/2, capsuleCollider.offset.y);
            yield return null;
        }
        StartCoroutine(spriteFade.fadeRoutine());
    }

    private void FaceMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }


}
