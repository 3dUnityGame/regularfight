using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenu : MonoBehaviour
{
    private AudioSource au;
    void Start()
    {
        au = GetComponent<AudioSource>();
    }

    public void ButtonSound()
    {
        if(PlayerPrefs.GetInt("mute") == 0)
            au.Play();
    }
}
