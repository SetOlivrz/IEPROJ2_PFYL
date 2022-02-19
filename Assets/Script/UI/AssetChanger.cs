using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AssetChanger : MonoBehaviour
{
    //public static AssetChanger instance = null;
    [SerializeField] private Material nightMaterial;
    [SerializeField] private Material dayMaterial;
    [SerializeField] private GameObject floor;
    [SerializeField] private Light playerLight;

/*    private void Awake()
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
    }*/

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 1 - Test")
        {
            if(floor == null)
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
            floor.GetComponent<MeshRenderer>().material = dayMaterial;
            playerLight.gameObject.SetActive(false);
            //change to day lights (R,G,B, Intensity)
            RenderSettings.ambientLight = new Color32(144, 142,110, 0);
            Camera.main.backgroundColor = new Color32(69, 73, 52, 255);
        }
        else
        {
            floor.GetComponent<MeshRenderer>().material = nightMaterial;
            playerLight.gameObject.SetActive(true);
            //change to night lights (R,G,B, Intensity)
            RenderSettings.ambientLight = new Color32(49, 16, 191, 1);
            Camera.main.backgroundColor = new Color32(27, 29, 29, 255);
        }
    }
}
