using Manager;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUI : UIHud
{   
    Button startButton;
    Button exitButton;
    Button creditButton;
    Button settingButton;
    
    // Start is called before the first frame update
    void Awake()
    {
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();
        settingButton = transform.Find("SettingButton").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
    }
    

    private void OnClickStartButton()
    {
        //StageSce입력     
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
    
    //셋팅 버튼 선택시 
    private void OnClickSettingButton()
    {
        UIManager.Instance.ShowPopup("SettingPopup");
    }

}
