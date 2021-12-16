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
    // Start is called before the first frame update
    void Start()
    {
        mainAudio = obj.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!mainAudio.isPlaying && mainAudio != null)
        {
            mainAudio.Play();
        }
    }
    public void OnMusicPlay(int choice)
    {
        if(choice == 1)
        {
            mainAudio.PlayOneShot(timeShift);
            mainAudio.clip = nightBGM;
            isMorning = false;
        }
        if(choice == 2)
        {
            mainAudio.PlayOneShot(timeShiftDay);
            isMorning = true;
            //to change for morning BGM
            mainAudio.clip = dayBGM;
        }
        if(choice == 3)
        {
            mainAudio.PlayOneShot(timeShift);
        }
    }

    public void onMusicStop()
    {
        mainAudio.Stop();
    }
}
