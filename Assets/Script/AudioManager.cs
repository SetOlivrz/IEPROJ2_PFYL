using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject obj;
    public AudioSource mainAudio;
    public AudioClip nightBGM;
    public AudioClip dayBGM;
    public AudioClip timeShift;
    public AudioClip timeShiftDay;
    public bool isMorning = true;
    public bool isNightPlaying = false;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        mainAudio = obj.GetComponent<AudioSource>();
        mainAudio.clip = dayBGM;
    }

    // Update is called once per frame
    void Update()
    {
        mainAudio.volume = 0.2f;
        if(!mainAudio.isPlaying && mainAudio != null && !isPaused)
        {
            mainAudio.Play();
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
}
