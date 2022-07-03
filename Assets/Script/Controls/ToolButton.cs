using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolButton : MonoBehaviour
{
    private Button button;
    private Vector2 buttonMidPoint;

    // Reference to Player Scripts
    private Player playerClass;
    private PlayerPlanting planting;
    
    // Current tool
    private ItemStack currentHeldItem;

    // Start is called before the first frame update
    void Start()
    {
        playerClass = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        planting = playerClass.GetComponent<PlayerPlanting>();

        button = GetComponent<Button>();
        buttonMidPoint = this.transform.position;
        button.onClick.AddListener(() => OnClickAction(Input.mousePosition));
    }

    // Shooting
    void OnClickAction(Vector2 mousePos)
    {
        currentHeldItem = playerClass.myInventory.GetInventoryStacks()[playerClass.GetSelectedHotbarIndex()];

        if (currentHeldItem.GetItem() != null)
        {
            // Tool
            if (currentHeldItem.GetItem() is Tool tool)
            {
                switch(tool.GetToolType())
                {
                    case Tool.ToolTypes.Hoe: HoeInput(); break;
                    case Tool.ToolTypes.WateringCan: WateringCanInput(); break;
                    case Tool.ToolTypes.Gun: GunInput(mousePos); break;
                }
            }
            // Plant Produce
            else if (currentHeldItem.GetItem() is PlantProduce plantProduce)
            {
                switch(plantProduce.GetProduceType())
                {
                    case PlantProduce.ProduceTypes.RoseSword: SwordInput(); break;
                }
            }
            // Seed
            else if (currentHeldItem.GetItem() is Seed seed)
            {
                SeedInput();
            }
        }
    }

    private void GunInput(Vector2 mousePos)
    {
        Vector2 heading = (buttonMidPoint - mousePos);
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;
        Debug.Log(direction);

        Shooting shooting = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        shooting.ShootInDirection(direction);
    }

    private void HoeInput()
    {
        planting.UseHoe();
    }

    private void WateringCanInput()
    {
        planting.UseWater();
    }

    private void SwordInput()
    {
        
    }

    private void SeedInput()
    {
        planting.UseSeed(currentHeldItem);
    }
}
