using UI.Popup;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : UIPopup
{
    [SerializeField]private Slider bgmSlider;
    [SerializeField]private Slider sfxSlider;

    private bool isMute = false;
    private float prevVolume;
    
    SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.Instance;
    }

    private void Start()
    {
        bgmSlider.value = soundManager.audioSource.volume;
        sfxSlider.value = soundManager.SFXVolume;
    }
    
    public void BGMMute()
    {
        bool isMute = soundManager.audioSource.mute;
        if (isMute)
        {
            bgmSlider.value =  soundManager.audioSource.volume;
        }
        else
        {
            bgmSlider.value = 0;
        }
        soundManager.audioSource.mute = !isMute;   
    }

    public void SFXMute()
    {
        
        prevVolume = soundManager.SFXVolume;
        sfxSlider.value = 0;
        SetSfxVolume(0);
    }
    
    public void SetBgmVolume(float volume)
    {
        soundManager.SetBgmVolume(volume);
    }
    public void SetSfxVolume(float volume)
    {
        soundManager.SetSfxVolume(volume);
    }
}
