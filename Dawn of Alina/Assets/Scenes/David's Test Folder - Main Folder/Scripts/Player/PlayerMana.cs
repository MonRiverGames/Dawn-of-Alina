using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    public Image barImage;
    private float maxMana = 100f;
    private float currentMana;
    private void Awake()
    {
        barImage.fillAmount = 100f;
    }

    public void SetMana(float manaCost)
    {
        barImage.fillAmount -= manaCost * Time.deltaTime;
    }

    public void RestoreMana(float manaRestore)
    {
        barImage.fillAmount += manaRestore * Time.deltaTime;
    }


}


