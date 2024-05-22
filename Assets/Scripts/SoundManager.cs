using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public List<AudioClip> radioClips;
    public AudioSource radioMusic;
    private int currentClipIndex = 0;

    public AudioSource BellSound;

    public AudioSource BuySound;
    public AudioSource DeclineSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Update()
    {
        if (!radioMusic.isPlaying && Shopper.soundOn)
        {
            PlayNextRadioClip();
        }
    }

    private void PlayNextRadioClip()
    {
        if (radioClips.Count == 0)
            return;

        int randomIndex = Random.Range(0, radioClips.Count);
        radioMusic.clip = radioClips[randomIndex];
        radioMusic.Play();
    }
}
