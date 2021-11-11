using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TimeBehavior : MonoBehaviour
{
    //Time
    private int day = 1;
    private int hour = 0;
    private float minute = 0.0f;

    //Light
    [SerializeField] GameObject sun;
    public static bool isDaytime = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(day == 7 && hour == 4 && minute >= 59.0f))
        {
            minute += Time.deltaTime * 5f/*2f*/;

            if (minute >= 60.0f)
            {
                if (!(day == 7 && hour == 4))
                {
                    hour++;
                    minute = 0.0f;
                }

                else
                {
                    minute = 59.0f;
                }
            }

            if (hour >= 4)
            {
                day++;
                hour = 0;
            }
        }

        if (hour == 2)
        {
            Quaternion nightLightRotation = new Quaternion(-10, -30, 0, 0);
            sun.transform.rotation = nightLightRotation;
            isDaytime = false;
        }

        if (hour == 4)
        {
            Quaternion nightLightRotation = new Quaternion(50, -30, 0, 0);
            sun.transform.rotation = nightLightRotation;
            isDaytime = true;
        }

        //timeText.text = "Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00");
        Debug.Log("Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00"));
    }
}
