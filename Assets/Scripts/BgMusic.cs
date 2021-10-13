using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusic : MonoBehaviour
{
    public static BgMusic BgInstance;
    public AudioSource Audio;

    private void Awake()
    {
        if(BgInstance !=null && BgInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        BgInstance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }
}
