using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerData : MonoBehaviour
{
    // im planning to use this script for storing player data that are needed for the saving mech implementation, but feel free to modify it as much as you can 
    public float maxHP = 100;
    public float currHP = 100;
    public float GOLD = 0;

    [SerializeField] Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        currHP = maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = currHP;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHP();
        UpdateGold();
    }

    void UpdateHP()
    {
        hpBar.maxValue = maxHP;
        hpBar.value = currHP;

        if (currHP <= 0)
        {
            currHP = 0;
            Debug.Log("Player died");
        }
    }

    void UpdateGold()
    {
        if (GOLD<=0)
        {
            GOLD = 0;
        }
    }

    public void TakeDamage(float damage)
    {
        currHP -= damage;
    }
}
