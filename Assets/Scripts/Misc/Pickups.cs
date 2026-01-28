using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    private enum PickupType
    {
        Coin,
        Health,
        Stamina
    }

    [SerializeField] private PickupType pickupType;
    [SerializeField] private float pickupDistance = 5f;
    [SerializeField] private float accelerationRate = 0.4f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private float moveSpeed = 10f;
    private Vector3 moveDir;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());    
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;   

        if(Vector3.Distance(transform.position, playerPos) < pickupDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelerationRate;
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0f;
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            DetectPickupType();
            Destroy(gameObject); 
        }
    }

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPos = transform.position;
        float randomx = transform.position.x + Random.Range(-2f,2f);
        float randomy = transform.position.y + Random.Range(-1f,1f);

        Vector2 endPos = new Vector2(randomx, randomy);

        float timePassed = 0f;

        while(timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heighT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heighT);
            transform.position = Vector2.Lerp(startPos, endPos, linearT) + new Vector2(0f, height);
            
            yield return null;
        }
    }

    private void DetectPickupType()
    {
        switch(pickupType)
        {
            case PickupType.Coin:
                EconomyManager.Instance.UpdateCurrentGold();
                break;
            case PickupType.Health:
                PlayerHealth.Instance.HealPlayer();
                break;
            case PickupType.Stamina:
                Stamina.Instance.RefreshStamina();
                break;
        }
    }
}
