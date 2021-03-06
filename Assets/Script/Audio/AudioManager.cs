using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    [Header("Sources")]
    [SerializeField] AudioSource mainAudio;
    [SerializeField] AudioSource effectsAudio;
    [SerializeField] AudioClip mainmenu;

    private Scene scene;
    private bool mmMusic = false;
    private bool isPaused = false;
    
    [Header("Options")]
    private float holder = 0.0f;
    public bool isMuted = false;
    public float volume = 1.0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            PlayMainBGM(mainmenu);
            mainAudio.volume = 0.2f;
            mmMusic = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        effectsAudio.PlayOneShot(clip);
    }

    public void PlayMainBGM(AudioClip clip)
    {
        mainAudio.clip = clip;
        mainAudio.Play();
    }

    public void PlayBGM(AudioClip transition, AudioClip bgm)
    {
        mainAudio.PlayOneShot(transition);
        mainAudio.clip = bgm;
    }
    // Update is called once per frame
    void Update()
    {
        if (!mainAudio.isPlaying && !isPaused && mainAudio != null)
        {
            mainAudio.Play();
        }
    }

    public void OnMusicStop()
    {
        mainAudio.Pause();
        effectsAudio.Stop();
    }

    public void OnGamePause()
    {
        OnMusicStop();
        isPaused = true;
    }

    public void OnGameResume()
    {
        isPaused = false;
        if(mainAudio.clip != null)
        {
            mainAudio.Play();
        }
    }

    public void OnVolumeChange(float value)
    {
        AudioListener.volume = value;
        volume = value;
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

    public void OnMain()
    {
        PlayMainBGM(mainmenu);
    }
}
