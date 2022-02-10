using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    private Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(val => AudioManager.instance.OnVolumeChange(val));
        scene = SceneManager.GetActiveScene();
        if(scene.name == "Level 1 - Test")
        {
            slider.value = AudioManager.instance.volume;
            text.text = (Mathf.Round(slider.value * 100.0f)).ToString() + "%";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.onValueChanged.AddListener(val => AudioManager.instance.OnVolumeChange(val));
        text.text = (Mathf.Round(slider.value * 100.0f)).ToString() + "%";
    }
}
