using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float musicVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchvariance;

    static private SoundManager instance;
    static public SoundManager Instance { get => instance; }

    public AudioSource audioSource;

    public AudioClip mainClip;

    public SoundSource soundSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
        audioSource.loop = true;
    }

    private void Start()
    {
        ChnageBackGroundMusic(mainClip);
    }

    public void ChnageBackGroundMusic(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    
    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(instance.soundSource);
        SoundSource _soundSource = obj.GetComponent<SoundSource>();
        _soundSource.PlaySound(clip, instance.soundEffectVolume, instance.soundEffectPitchvariance);
    }
}
