using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour
{

    [SerializeField] private Button stage1Button;
    [SerializeField] private Button stage2Button;
    [SerializeField] private Button stage3Button;

    [SerializeField] private Button mainButton;

    void Awake()
    {
        UpdateStageButton(stage1Button, 1);
        UpdateStageButton(stage2Button, 2);
        UpdateStageButton(stage3Button, 3);

        mainButton.onClick.AddListener(OnClickMainButton);
    }

    //스테이지버튼 초기화
    private void UpdateStageButton(Button button, int stageNumber)
    {
        StageState state = StageStateHandeler.GetStageState(stageNumber);
        Color buttonColor;

        //스테이지 클리어 상태에 따른 버튼 색 변화 및 활성화
        switch (state)
        {
            case StageState.NotOpen:
                button.interactable = false;
                buttonColor = Color.gray;
                break;
            case StageState.NotClear:
                button.interactable = true;
                buttonColor = Color.white;
                break;
            case StageState.Clear:
                button.interactable = true;
                buttonColor = Color.yellow;
                break;
            case StageState.PerfectClear:
                button.interactable = true;
                buttonColor = Color.magenta;
                break;
            default:
                buttonColor = Color.red;
                break;
        }

        button.GetComponent<Image>().color = buttonColor;
        button.onClick.AddListener(() => StageStateHandeler.LoadStage(stageNumber));
    }

    private void OnClickMainButton()
    {
        SceneManager.LoadScene("StartScene");
    }
}
