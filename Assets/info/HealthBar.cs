using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthBar : MonoBehaviourPun
{
    private Slider Slider;
    private Hero hero;
    private float fillValue;
    void Awake()
    {
        hero = GetComponentInParent<Hero>();
        Slider = GetComponent<Slider>();       
    }


    // Update is called once per frame
    void Update()
    {
         fillValue = hero.mCurrentHealth / hero.mMaxHealth;
        Slider.value = fillValue;
    }
}
