using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    private Button mainButton;
    private Slider soundSlider;
    private Button muteButton;
    private bool isMute = false;
    private float prevVolume;
    private AudioClip buttonClip;

    void Awake()
    {
        LoadAudio();
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
    private void LoadAudio()
    {
        buttonClip = Resources.Load<AudioClip>("_PopupButton");
        if (buttonClip == null)
        {
            Debug.LogError("클릭 사운드가 없습니다.");
        }
    }
    void OnClickMute()
    {
        
        SoundManager.Instance.audioSource.mute = !isMute;
        isMute = !isMute;
        
    }

    void OnClickMainButton()
    {
        SoundManager.PlayClip(buttonClip);
        this.gameObject.SetActive(false);
    }

    void SetBackGruondSoundVolume(float volume)
    {
        SoundManager.Instance.audioSource.volume = volume;
    }
}
