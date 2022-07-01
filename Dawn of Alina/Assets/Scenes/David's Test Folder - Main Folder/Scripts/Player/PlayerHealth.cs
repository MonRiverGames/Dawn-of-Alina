using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    
    public Image frontHealthBar;
    public Image backHealthBar;

    
    private InputManager inputManager;  //Delete this later (Test)

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        inputManager = GetComponent<InputManager>();  //Delete this later (Test)
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (inputManager.onFoot.TakeDamageTest.triggered)
        {
            TakeDamage(Random.Range(0, 10));
        }

        if (inputManager.onFoot.HealTest.triggered)
        {
            RestoreHealth(Random.Range(0, 10));
        }
    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if(fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;

            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }
    
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
