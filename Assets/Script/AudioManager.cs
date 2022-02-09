using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public GameObject obj;
    public AudioSource mainAudio;
    public AudioClip nightBGM;
    public AudioClip dayBGM;
    public AudioClip timeShift;
    public AudioClip timeShiftDay;
    public bool isMorning = false;
    public bool isNightPlaying = false;
    public bool isMainMenu = true;
    public bool isMuted = false;
    private bool isPaused = false;
    private bool triggered = false;
    
    [Header("Options")]
    [SerializeField] Text sliderText;
    private float holder = 0.0f;
    private float slideHolder = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        mainAudio = obj.GetComponent<AudioSource>();            
    }

    // Update is called once per frame
    void Update()
    {
        Scene current = SceneManager.GetActiveScene();
        // triggered is a temp stop gap to prevent the main audio from continuously playing
        // checker is also a temporary stop gap 
        if(current.name == "Level 1 - Test" && !triggered)
        {
            isMainMenu = false;
            mainAudio.clip = dayBGM;
            isMorning = true;
            triggered = true;
        }
        if(mainAudio != null)
        {
            if(!mainAudio.isPlaying && !isPaused)
            {
                mainAudio.Play();
            }
        }
        
    }
    public void OnMusicPlay(int choice)
    {
        if(choice == 1)
        {
            isMorning = false;
            isNightPlaying = true;
            mainAudio.PlayOneShot(timeShift);
            mainAudio.clip = nightBGM;
        }
        if(choice == 2)
        {
            mainAudio.PlayOneShot(timeShiftDay);
            isMorning = true;
            isNightPlaying = false;
            mainAudio.clip = dayBGM;
        }
    }

    public void OnMusicStop()
    {
        mainAudio.Stop();
    }

    public void OnGamePause()
    {
        mainAudio.Stop();
        isPaused = true;
    }

    public void OnGameResume()
    {
        isPaused = false;
    }

    public void OnVolumeChange(Slider slider)
    {
        mainAudio.volume = slider.value;
        slideHolder = Mathf.Round(slider.value * 100.0f);
        sliderText.text = slideHolder.ToString() + "%";
    }

    public void OnMute(bool muted)
    {
        if (muted)
        {
            holder = mainAudio.volume;
            mainAudio.volume = 0;
            isMuted = true;
        } 
        else
        {
            mainAudio.volume = holder;
            isMuted = false;
        }
    }
}
