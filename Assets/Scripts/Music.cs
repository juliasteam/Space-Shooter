using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    static Music _instance;
    public static Music instance
    {
        get
        {
            return _instance;
        }
    }
    private AudioSource _audio;
    public AudioSource audioSource
    {
        get
        {
            if (_audio == null)
                _audio = GetComponent<AudioSource>();
            return _audio;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            audioSource.Play();
        }
        else
        {
            if (_instance.audioSource.clip == this.audioSource.clip)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                audioSource.Stop();
                Destroy(_instance.gameObject);
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
                audioSource.Play();
            }
        }

    }

   
}
