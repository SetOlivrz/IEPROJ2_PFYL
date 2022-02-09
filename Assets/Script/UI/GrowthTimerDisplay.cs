using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowthTimerDisplay : MonoBehaviour
{
    [SerializeField] private float ticks = 0.0f;
    [SerializeField] private float maxValue = 1.0f;

    [SerializeField] private Image timerDisplay;

    [SerializeField] private GameObject soil;

    Soil soilScript;
    float val = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
       soilScript = soil.GetComponent<Soil>();
       timerDisplay.fillAmount =0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay(val);

    }

    private void UpdateDisplay(float val)
    {
        if (soilScript != null)
        {
            val = soilScript.gTicks / soilScript.plantGTime;

            if (soil == null)
            {
                Debug.Log("valu:  " + val);

            }
            else
            {
                Debug.Log("working");
                if (soilScript.gTicks >= soilScript.plantGTime)
                {
                    soilScript.gTicks = soilScript.plantGTime;
                    //Debug.Log("timeeee: "+soilScript.gTicks + " / " + soilScript.plantGTime);
                }
                else
                {
                    timerDisplay.fillAmount = val;
                    Debug.Log("GROWTH: " + soilScript.gTicks + " / " + soilScript.plantGTime + " || " + "value: " + val);
                }

            }
        }
    }
}
