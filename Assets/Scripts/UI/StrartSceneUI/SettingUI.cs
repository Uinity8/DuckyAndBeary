using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    Button mainButton;


    void Awake()
    {
        mainButton = transform.Find("MainButton").GetComponent<Button>();

        mainButton.onClick.AddListener(OnClickMainButton);
    }

    void OnClickMainButton()
    {
        this.gameObject.SetActive(false);
    }
}
