using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    private Button mainButton;
    private Slider soundSlider;
    private Button muteButton;
    private bool isMute = false;
    private float prevVolume;

    void Awake()
    {
        mainButton = transform.Find("MainButton").GetComponent<Button>();
        soundSlider = transform.Find("SoundSlider").GetComponent<Slider>();
        muteButton = transform.Find("MuteButton").GetComponent<Button>();

        muteButton.onClick.AddListener(OnClickMute);
        mainButton.onClick.AddListener(OnClickMainButton);
        soundSlider.onValueChanged.AddListener(SetBackGruondSoundVolume);
        
    }

    private void Start()
    {
        soundSlider.value = SoundManager.Instance.audioSource.volume;
    }

    void OnClickMute()
    {
        
        SoundManager.Instance.audioSource.mute = !isMute;
        isMute = !isMute;
        
    }

    void OnClickMainButton()
    {
        this.gameObject.SetActive(false);
    }

    void SetBackGruondSoundVolume(float volume)
    {
        SoundManager.Instance.audioSource.volume = volume;
    }
}
