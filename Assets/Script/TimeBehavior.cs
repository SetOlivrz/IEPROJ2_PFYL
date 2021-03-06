using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



using UnityEngine.Experimental.GlobalIllumination;

public class TimeBehavior : MonoBehaviour
{
    //Time
    public static int day = 1;
    public static float hour = 0; // set to 5 for debugging
    private float minute = 0.0f;
    private float accumMins = 0.0f;

    private float maxMins = 60.0f; // nMins it takes to be considered as an hour
    private float maxHours = 6.0f; // nHours it takes to be considered as a Day

    private const int maxDay = 8; // one full week cycle

    private float TIME_MULTIPLIER = 2.0f; // 3f for debugging // 2.0f normal

    //Light
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

    // variables for the tutorial
    [SerializeField] TutorialActionManager manager;
    [SerializeField] AssetChanger changer;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial Scene")
        {
            TIME_MULTIPLIER = 20.0f;
        }
        else // normal level
        {
            TIME_MULTIPLIER = 2.0f;
        }

        if (manager == null)
        {
            Debug.Log("Manager not Found");
        }
        Debug.Log("START");
        AudioManager.instance.PlayBGM(daytimeShift, dayBGM);
        //AssetChanger.instance.ChangeAssets(true);
        changer.ChangeAssets(true);
    }

    void BeginPlay()
    {
        if (manager == null)
        {
            Debug.Log("Manager not Found");
        }
        else
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
      
        if (!(day == maxDay && hour == 0 && minute >= 0)) //day == 7 && hour == 6 && minute >= 59.0f
        {
            if (manager != null)
            {
                if (manager.currentStep >= 29)
                {
                    UpdateTicks();
                    //transition for audio
                    AudioTransitionChecker();
                    UpdateHours();
                }
            }
            else
            {
                //Debug.Log("just do normal update");
                UpdateTicks();
                //transition for audio
                AudioTransitionChecker();
                UpdateHours();

            }
            
        }

        if (/*hour >= 2*/ stageClear)
        {
            day++;

            EnemySpawning.totalEnemyInLevel = 0;
            EnemySpawning.totalEnemyKilledInLevel = 0;
            AudioManager.instance.PlayBGM(daytimeShift, dayBGM);
            //AssetChanger.instance.ChangeAssets(true);
            changer.ChangeAssets(true);
            hour = 0;
            Debug.Log("day: " + day);
            stageClear = false;

        }


     
        if (hour == maxHours && isDaytime == true) // set to night when the hours needed is met
        {
            Debug.Log("Good Evening");
            isDaytime = false;
            changer.ChangeAssets(false);
            //AssetChanger.instance.ChangeAssets(false);
        }

        if (hour == 0 && !isDaytime) // if hours = 0  and its do set night time to day time
        {
            changer.ChangeAssets(true);
            //AssetChanger.instance.ChangeAssets(true);
            AudioManager.instance.PlayBGM(daytimeShift, dayBGM);
            isDaytime = true;
            nightLight.SetActive(false);
        }

        //Debug.Log("Day: " + day + "  Hour: " + hour + " " + "Minutes: " + minute);

        //timeText.text = "Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00");
        //Debug.Log("Day: " + day.ToString() + "\n" + hour.ToString("00") + ":" + (Mathf.Round(minute)).ToString("00"));

        dayLabel.text = "DAY " + day.ToString();

        if (manager != null)
        {
            if (manager.currentStep >= 29)
            {
                UpdateClock();
            }
        }
        else
        {
            UpdateClock();

        }


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
        if (minute < 60.0f && minute >= 40.0f)
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

           // Debug.Log("AM: " + accumMins + "/" + (maxMins * maxHours));
        }
        
        
        else if (isDaytime ==  false)
        {
            accumMins = 0;
            float angle = Mathf.Lerp(-180, -360, (EnemySpawning.totalEnemyKilledInLevel/EnemySpawning.totalEnemyInLevel));

            Quaternion target = Quaternion.Euler(0, 0, angle);
            clock.transform.rotation = Quaternion.Slerp(clock.transform.rotation, target, Time.deltaTime);

             //Debug.Log("night: killed " + EnemySpawning.totalEnemyKilledInLevel + "/: " + EnemySpawning.totalEnemyInLevel);

        }

    }
}