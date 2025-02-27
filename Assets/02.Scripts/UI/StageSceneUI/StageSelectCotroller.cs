using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectCotroller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private Button stageSelectButton;
    [SerializeField] private int stageIndex;
    [SerializeField] private GameObject lockImage;

    private void Awake()
    {
        //if (isClear || stageIndex == 1) 
        stageSelectButton.interactable = true;
        SetStageUI();
        SetIsLock();

        stageSelectButton.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Stage" + stageIndex);
    }

    private void SetStageUI()
    {
        //currentScore.text = score.ToString();
        //stageText.text = "STAGE" + stageIndex.ToString();

        GameResult result = GameManager.Instance.GetStageResult("Stage" + stageIndex);
        stageText.text = stageIndex.ToString();
    }

    private void SetIsLock()
    {
        GameResult result = GameManager.Instance.GetStageResult("Stage" + stageIndex);

        if (result.stageName == "Stage1")
            lockImage.SetActive(false);

        else lockImage.SetActive(result.stageStatus == StageStatus.Locked);

    }
}
