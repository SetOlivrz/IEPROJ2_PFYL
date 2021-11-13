using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryBase : MonoBehaviour, IInventoryItem
{
    public virtual string Name
    {
        get
        {
            return "_base item_";
        }
    }
    public Sprite _Image;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }
    public virtual void OnUse()
    {

    }
    public void onPickup()
    {
        gameObject.SetActive(false);
    }
    public void onDrop()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }
}
