using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour
{

    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;

    void Start()
    {
        UpdateStageButton(stage1Button, 1);
        UpdateStageButton(stage2Button, 2);
        UpdateStageButton(stage3Button, 3);
    }

    void UpdateStageButton(Button button, int stageNumber)
    {
        StageState state = StageManager.GetStageState(stageNumber);
        Color buttonColor;

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
        button.onClick.AddListener(() => StageManager.LoadStage(stageNumber));
    }
}
