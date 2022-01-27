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
    private float hour = 0; // set to 5
    private float minute = 0.0f;

    private float maxMins = 60.0f; // nMins it takes to be considered as an hour
    private float maxHours = 6.0f; // nHours it takes to be considered as a Day

    private const int maxDay = 8; // one full week cycle


    private const float TIME_MULTIPLIER = 3.0f;

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
  
        if (!(day == maxDay && hour == 0 && minute >= 0)) //day == 7 && hour == 6 && minute >= 59.0f
        {
            UpdateTicks();

            //transition for audio
            AudioTransitionChecker();

            UpdateHours();
        }

        if (/*hour >= 2*/ stageClear)
        {
            day++;
            audioManager.onMusicStop();
            hour = 0;
            Debug.Log("day: " + day);
            stageClear = false;
            
        }

        if (hour == maxHours && isDaytime == true) // set to night when the hours needed is met
        {
            Debug.Log("Good Evening");
            Vector3 nightLightRotation = new Vector3(-10, -30, 0);
            sun.transform.localEulerAngles = nightLightRotation;
            isDaytime = false;
        }

        if (hour == 0 && !isDaytime) // if hours = 0  and its do set night time to day time
        {
            audioManager.onMusicStop();
            audioManager.OnMusicPlay(2);

            Debug.Log("Good Morning");
            Vector3 nightLightRotation = new Vector3(50, -30, 0);
            sun.transform.localEulerAngles = nightLightRotation;
            isDaytime = true;
        }

        //Debug.Log("Day: " + day + "  Hour: " + hour + " " + "Minutes: " + minute);

        //timeText.text = "Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00");
        Debug.Log("Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00"));

        dayLabel.text = "DAY " + day.ToString();
    }

    private void UpdateHours()
    {
        if (minute >= maxMins)// increment the hour if minute is greater than max min
        {
            if (!(day == maxDay - 1 && hour == maxHours))
            {
                hour++; // increment hour
                minute = 0.0f; // reset minutes
            }
            else
            {
                minute = 59.0f;
            }
        }
    }

    private void AudioTransitionChecker()
    {
        if (minute < 60.0f && minute >= 51.0f)
        {
            //shift from day to night for audio
            if (hour + 1 == 6 && isDaytime)
            {
                if (audioManager.isMorning)
                {
                    audioManager.onMusicStop();
                }

                if (!audioManager.mainAudio.isPlaying)
                {
                    audioManager.OnMusicPlay(1);

                }
            }
        }
    }

    private void UpdateTicks()
    {
        if (isDaytime == true) // DAY checker
        {
            // Set to 2f to showcase audio transition accurately
            minute += Time.deltaTime * TIME_MULTIPLIER; //2f; Note: Use 30f for debugging 
        }
    }

    public void UpdateClock()
    {
        //clock.transform.rotation = Mathf.Lerp(0.0f, 360.0f, minute/hour);
    }
}
