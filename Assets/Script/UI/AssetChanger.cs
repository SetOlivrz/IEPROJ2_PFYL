using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssetChanger : MonoBehaviour
{
    public static AssetChanger instance = null;
    [SerializeField] private List<GameObject> dayAssets = new List<GameObject>();
    [SerializeField] private List<GameObject> nightAssets = new List<GameObject>();
    [SerializeField] private Material nightMaterial;
    [SerializeField] private Material dayMaterial;
    [SerializeField] private GameObject floor;
    [SerializeField] private Light playerLight;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAssets(bool isDaytime)
    {
        if (isDaytime)
        {
            foreach(GameObject night in nightAssets)
            {
                night.SetActive(false);
            }
            foreach (GameObject day in dayAssets)
            {
                day.SetActive(true);
            }
            floor.GetComponent<MeshRenderer>().material = dayMaterial;
            playerLight.gameObject.SetActive(false);
        }
        else
        {
            foreach (GameObject night in nightAssets)
            {
                night.SetActive(true);
            }
            foreach (GameObject day in dayAssets)
            {
                day.SetActive(false);
            }
            floor.GetComponent<MeshRenderer>().material = nightMaterial;
            playerLight.gameObject.SetActive(true);
        }
    }
}
