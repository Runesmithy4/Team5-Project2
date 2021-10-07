using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Needed to control slider on main menu

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()//Checks to see if any volume changes have been made to load
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()//Controls volume slider
    {
        AudioListener.volume = volumeSlider.value;//Value of sound is equal to value of current slider setting
        Save();
    }

    private void Load()//load volume setting between sessions
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()//Save volume settings between sessions
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
