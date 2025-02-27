using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float musicVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchvariance;

    private static SoundManager _instance;
    public static SoundManager Instance => _instance;

    public AudioSource audioSource;

    public AudioClip mainClip;

    public SoundSource soundSource;
    public float SfxVolume => soundEffectVolume;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
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
        ChangeBackGroundMusic(mainClip);
    }

    public void ChangeBackGroundMusic(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    
    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(_instance.soundSource);
        SoundSource _soundSource = obj.GetComponent<SoundSource>();
        _soundSource.PlaySound(clip, _instance.soundEffectVolume, _instance.soundEffectPitchvariance);
    }
    
    public void SetBgmVolume(float volume)
    {
        audioSource.volume = volume;
    }
    public void SetSfxVolume(float volume)
    {
        soundEffectVolume = volume;
    }
}
