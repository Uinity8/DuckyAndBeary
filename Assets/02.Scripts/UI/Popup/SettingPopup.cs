using UI.Popup;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : UIPopup
{
    [SerializeField]private Slider bgmSlider;
    [SerializeField]private Slider sfxSlider;
    
    [SerializeField]private Image bgmIcon;
    [SerializeField]private Image sfxIcon;
    
    [SerializeField]Sprite[] bgmSprites;
    [SerializeField]Sprite[] sfxSprites;

    public AudioClip vfxClip;
    
    private bool isSfxMute = false;
    private float prevSfxVolume;
    
    SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.Instance;
    }

    private void Start()
    {
        bgmSlider.value = soundManager.audioSource.volume;
        
        sfxSlider.value = soundManager.SfxVolume;
        prevSfxVolume = sfxSlider.value;
        
        bgmSlider.onValueChanged.AddListener(SetBgmVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        
    }
    
    public void BGMMute()
    {
        bool isMute = !soundManager.audioSource.mute;
        soundManager.audioSource.mute = isMute;  
        
        bgmIcon.sprite = isMute ? bgmSprites[0] : bgmSprites[1];

    }

    public void SfxMute()
    {
        isSfxMute = !isSfxMute;
        if (isSfxMute)
        {
            sfxIcon.sprite = sfxSprites[0];
            SetSfxVolume(0);   
        }
        else
        {
            sfxIcon.sprite = sfxSprites[1];
            SetSfxVolume(sfxSlider.value);
        }
    }
    
    public void SetBgmVolume(float volume)
    {
        soundManager.SetBgmVolume(volume);
    }
    public void SetSfxVolume(float volume)
    {
        soundManager.SetSfxVolume(volume);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!Mathf.Approximately(prevSfxVolume, sfxSlider.value))
            {
                prevSfxVolume = sfxSlider.value;
                SoundManager.PlayClip(vfxClip);
            }
        }
    }
}
