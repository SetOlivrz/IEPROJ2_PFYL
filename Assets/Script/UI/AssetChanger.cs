using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (SceneManager.GetActiveScene().name == "Level 1 - Test")
        {
            /*//Assign
            //if (TimeBehavior.isDaytime)
            //{
                dayAssets[0] = GameObject.FindGameObjectsWithTag("Assets")[0];
                dayAssets[1] = GameObject.FindGameObjectsWithTag("Assets")[1];
                dayAssets[2] = GameObject.FindGameObjectsWithTag("Assets")[2];
            //}

            //if (!TimeBehavior.isDaytime)
            //{
                nightAssets[0] = GameObject.FindGameObjectsWithTag("Assets")[0];
                nightAssets[1] = GameObject.FindGameObjectsWithTag("Assets")[1];
                nightAssets[2] = GameObject.FindGameObjectsWithTag("Assets")[2];
            //}*/
            
            floor = GameObject.FindGameObjectWithTag("Ground");

            /*if (GameObject.FindGameObjectWithTag("PointLight").activeInHierarchy)
                playerLight = GameObject.FindGameObjectWithTag("PointLight").GetComponent<Light>();
            else playerLight = null;*/
        }
    }

    public void ChangeAssets(bool isDaytime)
    {
        if (isDaytime)
        {
            /*foreach(GameObject night in nightAssets)
            {
                night.SetActive(false);
            }
            foreach (GameObject day in dayAssets)
            {
                day.SetActive(true);
            }*/
            floor.GetComponent<MeshRenderer>().material = dayMaterial;
            playerLight.gameObject.SetActive(false);
            //change to day lights (R,G,B, Intensity)
            RenderSettings.ambientLight = new Color32(144, 142,110, 0);
        }
        else
        {
            //Disabled for now, switching of assets may not be necessary
            /*foreach (GameObject night in nightAssets)
            {
                night.SetActive(true);
            }
            foreach (GameObject day in dayAssets)
            {
                day.SetActive(false);
            }*/
            floor.GetComponent<MeshRenderer>().material = nightMaterial;
            playerLight.gameObject.SetActive(true);
            //change to night lights (R,G,B, Intensity)
            RenderSettings.ambientLight = new Color32(49, 16, 191, 1);
        }
    }
}
