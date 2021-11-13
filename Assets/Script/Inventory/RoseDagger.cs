using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseDagger : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Rose";
        }
    }
    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void onPickup()
    {
        gameObject.SetActive(false);
    }
    public void onDrop()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }
}
