using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TimeBehavior : MonoBehaviour
{
    //Time
    public static int day = 1;
    private int hour = 0;
    private float minute = 0.0f;

    //Light
    [SerializeField] GameObject sun;
    public static bool isDaytime = true;

    //Controls
    public static bool stageClear = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(day == 8 && hour == 0 && minute >= 0)) //day == 7 && hour == 6 && minute >= 59.0f
        {
            if (isDaytime)
            {
                minute += Time.deltaTime * 2f; //2f; Note: Use 30f for debugging
            }

            if (minute >= 60.0f)
            {
                if (!(day == 7 && hour == 6))
                {
                    hour++;
                    minute = 0.0f;
                }

                else
                {
                    minute = 59.0f;
                }
            }
        }

        if (/*hour >= 2*/ stageClear)
        {
            day++;
            hour = 0;
            Debug.Log("day: " + day);
            stageClear = false;
        }

        if (hour == 6 && isDaytime)
        {
            Debug.Log("Good Evening");
            Vector3 nightLightRotation = new Vector3(-10, -30, 0);
            sun.transform.localEulerAngles = nightLightRotation;
            isDaytime = false;
        }

        if (hour == 0 && !isDaytime)
        {
            Debug.Log("Good Morning");
            Vector3 nightLightRotation = new Vector3(50, -30, 0);
            sun.transform.localEulerAngles = nightLightRotation;
            isDaytime = true;
        }

        //timeText.text = "Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00");
        //Debug.Log("Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00"));
    }
}
