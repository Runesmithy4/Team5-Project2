using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMusic : MonoBehaviour
{
    public Text toggleMusictct;

    private void Start()
    {
        if (BgMusic.BgInstance.Audio.isPlaying)
        {
            toggleMusictct.text = "Background Music On";
        }
        else
        {
            toggleMusictct.text = "Background Music Off";
        }
    }

    public void MusicToggle()
    {
        if (BgMusic.BgInstance.Audio.isPlaying)
        {
            BgMusic.BgInstance.Audio.Pause();
            toggleMusictct.text = "Background Music Off";
        }
        else
        {
            BgMusic.BgInstance.Audio.Play();
            toggleMusictct.text = "Background Music On";
        }
    }
}
