using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Experimental.GlobalIllumination;

public class TimeBehavior : MonoBehaviour
{
    //Time
    public static int day = 1;
    private float hour = 5; // set to 5 for debugging
    private float minute = 0.0f;
    //Audio
    public AudioManager audioManager;

    //Light
    [SerializeField] GameObject sun;
    public static bool isDaytime = true;

    //Controls
    public static bool stageClear = false;

    // test
    [SerializeField] Transform clock;

    [SerializeField] Text dayLabel;

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
                // Set to 2f to showcase audio transition accurately
                minute += Time.deltaTime * 2f; //2f; Note: Use 30f for debugging 
            }
            //transition for audio
            if(minute < 60.0f && minute >= 55.0f)
            {
                //shift from day to night
                if(hour + 1 == 6 && isDaytime)
                {
                    if (audioManager.isMorning && !audioManager.isNightPlaying)
                    {
                        audioManager.OnMusicStop();
                    }
                    if (!audioManager.mainAudio.isPlaying)
                    {
                        audioManager.OnMusicPlay(1);
                    }
                }
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
            audioManager.OnMusicStop();
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
            audioManager.OnMusicStop();
            audioManager.OnMusicPlay(2);
            Debug.Log("Good Morning");
            Vector3 nightLightRotation = new Vector3(50, -30, 0);
            sun.transform.localEulerAngles = nightLightRotation;
            isDaytime = true;
        }

        //Debug.Log("Day: " + day + "  Hour: " + hour + " " + "Minutes: " + minute);

        //timeText.text = "Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00");
        //Debug.Log("Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00"));

        dayLabel.text = "DAY " + day.ToString();
    }


    public void UpdateClock()
    {
        //clock.transform.rotation = Mathf.Lerp(0.0f, 360.0f, minute/hour);
    }
}
