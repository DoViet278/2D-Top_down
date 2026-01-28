using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] private Sprite fullStaminaSprite, emptyStaminaSprite;
    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;
    const string STAMINA_UI_PATH = "StaminaContainer";

    protected override void Awake()
    {
        base.Awake();
        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }

    private void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_UI_PATH).transform;
        UpdateSaminaUI();
    }

    public void UseStamina() 
    {
        CurrentStamina--;
        UpdateSaminaUI();
    }

    public void RefreshStamina() 
    {
        if (CurrentStamina < maxStamina && !PlayerHealth.Instance.isDead) 
        {
            CurrentStamina++;
        }
        UpdateSaminaUI();
    }

    public void ReplenishStaminaOnDeath() 
    {
        CurrentStamina = startingStamina;
        UpdateSaminaUI();
    }

    private IEnumerator RefreshStaminaRoutine() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

    private void UpdateSaminaUI() 
    {
        for (int i = 0; i < maxStamina; i++) 
        {
            if(i <= CurrentStamina - 1)
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaSprite;
            }
            else
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaSprite;
            }
        }

        if(CurrentStamina < maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
