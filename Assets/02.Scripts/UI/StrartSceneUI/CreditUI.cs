using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditUI : MonoBehaviour
{
    private AudioClip buttonClip;
    Button mainButton;


    void Awake()
    {
        LoadAudio();
        mainButton = transform.Find("MainButton").GetComponent<Button>();

        mainButton.onClick.AddListener(OnClickMainButton);
    }
    private void LoadAudio()
    {
        buttonClip = Resources.Load<AudioClip>("_PopupButton");
        if (buttonClip == null)
        {
            Debug.LogError("클릭 사운드가 없습니다.");
        }
    }

    void OnClickMainButton()
    {
        SoundManager.PlayClip(buttonClip);
        this.gameObject.SetActive(false);
    }
}
