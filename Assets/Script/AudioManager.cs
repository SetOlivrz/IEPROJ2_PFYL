using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject obj;
    public AudioSource mainAudio;
    public AudioClip nightBGM;
    public AudioClip timeShift;
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
        switch (choice)
        {
            case 1: mainAudio.clip = nightBGM;
                if (mainAudio.isPlaying)
                    onMusicStop();
                break;
            case 2: mainAudio.clip = timeShift; 
                if (mainAudio.isPlaying)
                    onMusicStop(); 
                break;
            default: mainAudio.Stop();break;
        }
    }

   public void onMusicStop()
    {
        mainAudio.Stop();
    }
}
