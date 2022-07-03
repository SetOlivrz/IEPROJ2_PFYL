using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolButton : MonoBehaviour
{
    private Button button;
    private Vector2 buttonMidPoint;

    private Player playerClass;

    //Current tool
    private ItemStack currentHeldItem;

    // Start is called before the first frame update
    void Start()
    {
        playerClass = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            string itemName = currentHeldItem.GetItem().ItemName;
            // Gun
            if (itemName == "Gun")
            {
                Vector2 heading = (buttonMidPoint - mousePos);
                float distance = heading.magnitude;
                Vector2 direction = heading / distance;
                Debug.Log(direction);

                Shooting shooting = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
                shooting.ShootInDirection(direction);
            }
            else if (itemName == "Hoe")
            {
                Debug.Log("Hoe!");
            }
            else if (itemName == "WateringCan")
            {
                Debug.Log("Watering!");
            }
            else if (itemName == "Rose")
            {
                Debug.Log("Rose!");
            }
            else if (itemName == "RoseSword")
            {
                Debug.Log("Sword!");
            }
        }
    }
}
