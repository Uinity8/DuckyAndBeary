using Manager;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStage : MonoBehaviour

{
    private const string Stage = "Stage";

    //UI 설정값
    [SerializeField] private Button stageSelectButton;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI stageText;

    //스테이지 정보값
    [SerializeField] private int stageIndex;
    public int StageIndex { get => stageIndex;}
    public bool isClear = false;
    private int score;

    public GameStage(int _stageIndex, bool _isClear, int _score)
    {
        stageIndex = _stageIndex;
        isClear = _isClear;
        score = _score;
    }

    private void Awake()
    {
        //if (isClear || stageIndex == 1) 
            stageSelectButton.interactable = true;
        //else stageSelectButton.interactable = false;

        SetStageUI();
        stageSelectButton.onClick.AddListener(LoadScene);
        
    }

    //스테이지 index 확인 후 로드씬
    private void LoadScene()
    {
        SceneManager.LoadScene(Stage + stageIndex);
    }

    //스코어 저장
    private void SetStageUI()
    {
        // 게임 매니저에서 점수 가져오기
        if (GameManager.Instance != null) score = GameManager.Instance.Score;
        else score =0;

        currentScore.text = score.ToString();
        stageText.text = "STAGE" + stageIndex.ToString();
    }
}
