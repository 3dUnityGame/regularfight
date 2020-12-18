using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("C"))
            PlayerPrefs.SetInt("C", 1);
        if (!PlayerPrefs.HasKey("M"))
            PlayerPrefs.SetInt("M", 1);

        if (!PlayerPrefs.HasKey("mute"))
        {
            PlayerPrefs.SetInt("mute", 0);
        }
        else if(PlayerPrefs.GetInt("mute") == 1)
        {
            transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
        }
    }

    public void Go()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MuteOrUnmute()
    {
        if(PlayerPrefs.GetInt("mute") == 0)
        {
            transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(4).GetChild(1).gameObject.SetActive(false);
        }
    }
}
