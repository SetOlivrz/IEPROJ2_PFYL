using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioClip buttonClip;

    public void ButtonSFX()
    {
        AudioManager.instance.PlaySound(buttonClip);
    }
}
