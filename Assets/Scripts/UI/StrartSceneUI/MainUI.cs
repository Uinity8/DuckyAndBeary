using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{   
    Button startButton;
    Button exitButton;
    Button creditButton;
    Button settingButton;

    [SerializeField] private GameObject creditUI;
    [SerializeField] private GameObject settingUI;
    // Start is called before the first frame update
    void Awake()

    {
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();
        creditButton = transform.Find("CreditButton").GetComponent<Button>();
        settingButton = transform.Find("SettingButton").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        creditButton.onClick.AddListener(OnClickCreditButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
    }

    private void Start()
    {
        creditUI.SetActive(false);
        settingUI.SetActive(false);
    }

    // Update is called once per frame
    void OnClickStartButton()
    {
        //StageSce입력
        SceneManager.LoadScene("Stage1");
    }

    void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void OnClickCreditButton()
    {
        creditUI.SetActive(true);
    }

    void OnClickSettingButton()
    {
        settingUI.SetActive(true);
    }

}
