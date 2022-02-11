using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



using UnityEngine.Experimental.GlobalIllumination;

public class TimeBehavior : MonoBehaviour
{
    //Time
    public static int day = 1;
    private float hour = 0; // set to 5 for debugging
    private float minute = 0.0f;
    private float accumMins = 0.0f;

    private float maxMins = 60.0f; // nMins it takes to be considered as an hour
    private float maxHours = 6.0f; // nHours it takes to be considered as a Day

    private const int maxDay = 8; // one full week cycle

    // lights
    private float lighTicks = 0.0f;
    private float maxLightAngle = 30.0f;

    private const float TIME_MULTIPLIER = 20.0f; // 3f for debugging

    //Light
    [SerializeField] GameObject sun;
    public static bool isDaytime = true;

    [SerializeField]
    GameObject nightLight;

    //Controls
    public static bool stageClear = false;

    // test
    [SerializeField] Transform clock;

    [SerializeField] Text dayLabel;

    [Header("Audio")]
    [SerializeField] AudioClip dayBGM;
    [SerializeField] AudioClip nightBGM;
    [SerializeField] AudioClip nightTimeShift;
    [SerializeField] AudioClip daytimeShift;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBGM(daytimeShift, dayBGM);
        day = 1;
        hour = 0;
        nightLight.SetActive(false);
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

            EnemySpawning.totalEnemyInLevel = 0;
            EnemySpawning.totalEnemyKilledInLevel = 0;
            AudioManager.instance.PlayBGM(daytimeShift, dayBGM);
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
            nightLight.SetActive(true);
        }

        if (hour == 0 && !isDaytime) // if hours = 0  and its do set night time to day time
        {
            AudioManager.instance.PlayBGM(daytimeShift, dayBGM);
            Debug.Log("Good Morning");
            Vector3 nightLightRotation = new Vector3(50, -30, 0);
            sun.transform.localEulerAngles = nightLightRotation;
            isDaytime = true;
            nightLight.SetActive(false);
        }

        //Debug.Log("Day: " + day + "  Hour: " + hour + " " + "Minutes: " + minute);

        //timeText.text = "Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00");
        //Debug.Log("Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00"));

        dayLabel.text = "DAY " + day.ToString();

        UpdateClock();


        // update light
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
        if (minute < 60.0f && minute >= 45.0f)
        {
            //shift from day to night for audio
            if (hour + 1 == 6 && isDaytime)
            {
                AudioManager.instance.PlayBGM(nightTimeShift, nightBGM);
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
        if (isDaytime == true)
        {
            accumMins += Time.deltaTime * TIME_MULTIPLIER;
            float angle = Mathf.Lerp(0.0f, -180, accumMins/ (maxMins * maxHours));

            Quaternion target = Quaternion.Euler(0, 0, angle);

            clock.transform.rotation = Quaternion.Slerp(clock.transform.rotation, target, Time.deltaTime * 5.0f);

            //Debug.Log("AM: " + accumMins + "/" + (maxMins * maxHours));
        }
        
        
        else if (isDaytime ==  false)
        {
            accumMins = 0;
            float angle = Mathf.Lerp(-180, -360, (EnemySpawning.totalEnemyKilledInLevel/EnemySpawning.totalEnemyInLevel));

            Quaternion target = Quaternion.Euler(0, 0, angle);
            clock.transform.rotation = Quaternion.Slerp(clock.transform.rotation, target, Time.deltaTime);

             Debug.Log("night: killed " + EnemySpawning.totalEnemyKilledInLevel + "/: " + EnemySpawning.totalEnemyInLevel);

        }

    }
}