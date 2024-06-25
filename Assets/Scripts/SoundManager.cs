using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("----- Audio Source -------")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("----- Audio Clip -------")]
    public AudioClip background;
    public AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {
       musicSource.clip = background;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
