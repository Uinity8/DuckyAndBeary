using UI.Popup;
using UnityEngine.UI;

public class SettingPopup : UIPopup
{
    private Slider soundSlider;

    private bool isMute = false;
    private float prevVolume;
    
    SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.Instance;
    }

    private void Start()
    {
        soundSlider.value = soundManager.audioSource.volume;
    }

    void VFXMute()
    {
        soundSlider.value = 0;
        isMute = !isMute;
        soundManager.audioSource.mute = isMute;
    }
    
    void SetBGMVolume(float volume)
    {
        soundManager.SetBGMVolume(volume);
    }
    void SetSFXVolume(float volume)
    {
        soundManager.SetSFXVolume(volume);
    }
}
