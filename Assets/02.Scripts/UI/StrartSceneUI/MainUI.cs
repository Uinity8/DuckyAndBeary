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
    private void OnClickStartButton()
    {
        //StageSce입력
        
        SoundManager.Instance.ChnageBackGroundMusic(SoundManager.Instance.stageClip);
        SceneManager.LoadScene("StageSelect");
    }

    private void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //크리팃 버튼 선택시 
    private void OnClickCreditButton()
    {
        creditUI.SetActive(true);
    }

    //셋팅 버튼 선택시 
    private void OnClickSettingButton()
    {
        settingUI.SetActive(true);
    }

}
