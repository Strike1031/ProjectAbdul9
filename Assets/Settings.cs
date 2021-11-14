using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{


    public bool isMusic = false;

    public void Start()
    {
        if(isMusic && PlayerPrefs.HasKey("Music"))
        {
            if(PlayerPrefs.GetString("Music") == "On")
            {
                gameObject.GetComponent<Toggle>().SetIsOnWithoutNotify(true);
            } else if (PlayerPrefs.GetString("Music") == "Off")
            {
                gameObject.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
            }

        }
    }

    public void SwapMusic()
    {
        if (isMusic)
        {
            if (PlayerPrefs.GetString("Music") == "On")
            {
                DisableMusic();
            }
            else if (PlayerPrefs.GetString("Music") == "Off")
            {
                EnableMusic();
            } else
            {
                DisableMusic();
            }
        }
    }

    public void EnableMusic()
    {
        PlayerPrefs.SetString("Music", "On");
        FindObjectOfType<AudioMgr>().UpdateMusic();
    }

    public void DisableMusic()
    {
        PlayerPrefs.SetString("Music", "Off");
        FindObjectOfType<AudioMgr>().UpdateMusic();
    }


    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
