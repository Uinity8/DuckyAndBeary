using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetInGame : MonoBehaviour
{
    //UI 설정값
    [SerializeField] private Button stageSelectButton;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private int stageIndex;
    public int StageIndex { get => stageIndex; }
    private int score;

    private void Awake()
    {
        //if (isClear || stageIndex == 1) 
        stageSelectButton.interactable = true;

        SetStageUI();
        stageSelectButton.onClick.AddListener(LoadScene);
    }

    //스테이지 index 확인 후 로드씬
    private void LoadScene()
    {
        SceneManager.LoadScene("Stage" + stageIndex);
    }

    //스코어 저장
    private void SetStageUI()
    {
        //currentScore.text = score.ToString();
        //stageText.text = "STAGE" + stageIndex.ToString();

        GameResult result = GameManager.Instance.GetStageResult("Stage" + stageIndex);
        stageText.text = result.stageStatus.ToString();
    }
}
