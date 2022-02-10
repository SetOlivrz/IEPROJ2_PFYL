using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public enum Weapon
    {
        None,
        RoseDagger
    }

    enum Action
    {
        None,
        Cooldown,
        Attack,
        DealDamage
    }

    private EnemyBehavior enemy;

    //Ticks
    private float ticks = 0.0f;
    private float INTERVAL = 0.5f;

    //Sprites
    [SerializeField] Player player;
    [SerializeField] private PlayerController playerController;

    //Hand
    private GameObject defaultHand;
    private GameObject rightHand;
    private GameObject leftHand;

    public Weapon equippedWeapon = Weapon.None;
    private Action currentAction = Action.None;

    // Start is called before the first frame update
    void Start()
    {
        defaultHand = player.transform.GetChild(0).gameObject;
        rightHand = player.transform.GetChild(1).gameObject;
        leftHand = player.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ItemStack currentHeldItem = player.myInventory.GetInventoryStacks()[player.GetSelectedHotbarIndex()];

        //Equipping weapon
        if (currentHeldItem.GetItem() is PlantProduce produce)
        {
            switch (produce.GetProduceType())
            {
                case PlantProduce.ProduceTypes.RoseSword:
                    player.GetComponent<PlayerCombat>().equippedWeapon = PlayerCombat.Weapon.RoseDagger;
                    break;
            }
        }

        if (currentHeldItem.GetItem() != null)
        {
            if (currentHeldItem.GetItem().name != "RoseSword")
            {
                equippedWeapon = Weapon.None;
            }
        }
        else
        {
            equippedWeapon = Weapon.None;
        }

        if (equippedWeapon == Weapon.RoseDagger)
        {
            ChangeEquippedSprite();

            if (Input.GetMouseButtonDown(0) && !player.isOpen)
            {
                ChangeSlot();
                currentAction = Action.Attack;
            } 

            if(currentAction == Action.Attack)
            {
                //defaultHand.GetComponent<SpriteRenderer>().color = Color.red;
                //rightHand.GetComponent<SpriteRenderer>().color = Color.red;
                //leftHand.GetComponent<SpriteRenderer>().color = Color.red;
                //currentAction = Action.Cooldown;

                if (enemy != null)
                {

                    if(enemy.enemyName == "Normal Zombie")
                    enemy.ReceiveDamage(50);
                    else enemy.ReceiveDamage(20);

                    if (currentHeldItem.GetItem() is PlantProduce plantProduce)
                    {
                        currentHeldItem.DecreaseAmount(1);
                        if (currentHeldItem.GetCount() < 1)
                        {
                            currentHeldItem.RemoveItem();
                        }

                        InventoryManager.INSTANCE.OpenContainer(new ContainerPlayerHotbar(null, player.myInventory));
                    }

                    enemy = null;
                }
            }

            //if (currentAction == Action.Cooldown)
            //{
            //    ticks += Time.deltaTime;
            //}

            //if (ticks > INTERVAL)
            //{
            //    defaultHand.GetComponent<SpriteRenderer>().color = Color.white;
            //    rightHand.GetComponent<SpriteRenderer>().color = Color.white;
            //    leftHand.GetComponent<SpriteRenderer>().color = Color.white;
            //    ticks = 0.0f;
            //    currentAction = Action.None;
            //}
        }

        else
        {
            EmptyHand();
        }
    }

    void ChangeEquippedSprite()
    {
        ItemStack currentHeldItem = player.myInventory.GetInventoryStacks()[player.GetSelectedHotbarIndex()];

        if (currentHeldItem.GetItem() != null)
        {
            defaultHand.GetComponent<SpriteRenderer>().sprite = currentHeldItem.item.ItemIcon;
            rightHand.GetComponent<SpriteRenderer>().sprite = currentHeldItem.item.ItemIcon;
            leftHand.GetComponent<SpriteRenderer>().sprite = currentHeldItem.item.ItemIcon;
        }
    }

    void ChangeSlot()
    {
        if (playerController.isRight)
        {
            defaultHand.SetActive(false);
            rightHand.SetActive(true); 
            leftHand.SetActive(false);
        }

        else if (!playerController.isRight)
        {
            defaultHand.SetActive(false);
            rightHand.SetActive(false);
            leftHand.SetActive(true);
        }

        else if (playerController.isUpwards || !playerController.isUpwards)
        {
            defaultHand.SetActive(true);
            rightHand.SetActive(false);
            leftHand.SetActive(false);
        }

        StartCoroutine("DisableItem");
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.gameObject.GetComponent<EnemyBehavior>();
        }
    }

    public void OnTriggerExit()
    {
        enemy = null;
    }

    public IEnumerator DisableItem()
    {
        yield return new WaitForSeconds(0.3f);
        EmptyHand();
    }

    void EmptyHand()
    {
        ItemStack currentHeldItem = player.myInventory.GetInventoryStacks()[player.GetSelectedHotbarIndex()];
        defaultHand.SetActive(false);
        rightHand.SetActive(false);
        leftHand.SetActive(false);
    }
}
