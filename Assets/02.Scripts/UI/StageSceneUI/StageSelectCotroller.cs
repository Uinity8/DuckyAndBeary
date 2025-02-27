using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using System;

public class StageSelectCotroller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private Button stageSelectButton;  
    [SerializeField] private GameObject lockImage;
    [SerializeField] private int stageIndex;
    [SerializeField] private GameObject[] stars;

    GameResult gameResult;
    private StageStatus stageStatus;
    private int score;
    public bool isLock = false;
    private void Awake()
    {
        stageSelectButton.onClick.AddListener(LoadScene);

        Init();
          
    }

    private void Start()
    {
        SetStageUI();
        SetInteract();
        SetClearStar();
    }

    private void Init()
    {
        gameResult = GameManager.Instance.GetStageResult("Stage" + stageIndex);
        score = gameResult.score;
        stageStatus = gameResult.stageStatus;
        if (stageIndex == 1) isLock = false;
        else isLock = gameResult.stageStatus == StageStatus.Locked;
    }
        
    private void SetInteract()
    {
        lockImage.SetActive(isLock);
        if (isLock)
        {
            stageSelectButton.interactable = false;
        }
    }

    private void SetStageUI()
    {
        gameResult = GameManager.Instance.GetStageResult("Stage" + stageIndex);
        stageText.text = stageIndex.ToString();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Stage" + stageIndex);
    }

    private void SetClearStar()
    {
        int starScore = gameResult.score;

        int maxStar = Math.Min(starScore, stars.Length);

        for (int i = 0; i < starScore; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
