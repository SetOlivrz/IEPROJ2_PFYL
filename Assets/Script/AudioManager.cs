using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject obj;
    private AudioSource mainAudio;
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
        if(choice == 1)
        {
            mainAudio.PlayOneShot(timeShift);
            mainAudio.clip = nightBGM;
        }
        if(choice == 2)
        {
            mainAudio.PlayOneShot(timeShift);
            //to change for morning BGM
            //mainAudio.clip = nightBGM;
        }
    }

   public void onMusicStop()
    {
        mainAudio.Stop();
    }
}
