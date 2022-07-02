using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerData : MonoBehaviour
{
    // im planning to use this script for storing player data that are needed for the saving mech implementation, but feel free to modify it as much as you can 
    public float maxHP = 100;
    public float currHP = 100;
    public int GOLD = 100;

    [SerializeField] Slider hpBar;
    [SerializeField] Text goldLabel;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip playerHit;

    [SerializeField] ButtonManager uiManager;
    //Damage Animation
    bool displayed = false;
    bool isDamaged = false;
    float dticks = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial Scene")
        {
            maxHP = 500;
        }
        currHP = maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = currHP;
        uiManager = GameObject.FindGameObjectWithTag("ButtonManager").GetComponent<ButtonManager>();
        displayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHP();
        UpdateGold();
        if (isDamaged)
        {
            dticks += Time.deltaTime;
            if(dticks > 1f)
            {
                dticks = 0;
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                isDamaged = false;
            }
        }
    }

    void UpdateHP()
    {
        hpBar.maxValue = maxHP;
        hpBar.value = currHP;
        if(currHP <= 10)
        {
            //animator.SetBool("hurt", true);
            if (currHP <= 0)
            {
                if (SceneManager.GetActiveScene().name == "Tutorial Scene")
                {
                    currHP = maxHP;
                }
                else
                {
                    currHP = 0;
                    if (!displayed)
                    {
                        Debug.Log("Player died");
                        uiManager.GameOverPopup();
                        displayed = true;
                    }
                }
            }
        }
        
    }

    void UpdateGold()
    {
        if (GOLD<=0)
        {
            GOLD = 0;
        }

        goldLabel.text = GOLD.ToString();
    }

    public void TakeDamage(float damage)
    {
        currHP -= damage;
        //this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        isDamaged = true;
        StartCoroutine("PlayHurtAnimation");
    }

    IEnumerator PlayHurtAnimation()
    {
        if (playerHit != null)
            AudioManager.instance.PlaySound(playerHit);
        animator.SetBool("left", false);
        animator.SetBool("right", false);
        animator.SetBool("front", false);
        animator.SetBool("back", false);
        animator.SetBool("hurt", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("hurt", false);
    }

    public void addGold(int amnt)
    {
        GOLD += amnt;
    }
}
