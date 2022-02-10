using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(val => AudioManager.instance.OnVolumeChange(val));
    }

    // Update is called once per frame
    void Update()
    {
        slider.onValueChanged.AddListener(val => AudioManager.instance.OnVolumeChange(val));
        text.text = (Mathf.Round(slider.value * 100.0f)).ToString() + "%";
    }
}
