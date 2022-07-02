using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerPlayerHotbarSelector : MonoBehaviour
{
    private Player player;
    private GameObject container;
    [SerializeField]private List<Slot> items;

    void Start()
    {
        player = FindObjectOfType<Player>();

        container = transform.parent.GetChild(0).gameObject;

        foreach (Transform child in container.transform)
        {
            items.Add(child.GetComponent<Slot>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && container != null)
        {
            int selectedIndex = player.GetSelectedHotbarIndex();
            if (items.Count == 0) return;

            for(int i = 0; i < items.Count; i++)
            {
                items[i].ToggleSelector(false);
            }

            items[selectedIndex].ToggleSelector(true);
        }
    }
}
