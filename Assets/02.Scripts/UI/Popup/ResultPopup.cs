using Manager;
using Scripts.UI.StageSceneUI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace UI.Popup
{
    public class ResultPopup : GameControlPopup
    {
        GameStage clearInfo;
        [SerializeField] TextMeshProUGUI stageClearCheck;
        [SerializeField] TextMeshProUGUI gemNumText;
        [SerializeField] TextMeshProUGUI gemNumCheck;
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] TextMeshProUGUI timeCheck;

        [SerializeField] Button nextStageButton;

        public override void Initialize()
        {
            base.Initialize();
            //게임 클리어 정보 불러오기
            clearInfo = GameManager.Instance.GetCurrentStageInfo();
            Debug.Log($"{clearInfo.StageName} / {clearInfo.Score} / {clearInfo.ClearTime}");
            ShowStageResult();

        }

        void ShowStageResult()
        {
            stageClearCheck.text = "Clear";
            gemNumText.text = clearInfo.Score.ToString();
            gemNumCheck.text = clearInfo.Score >= clearInfo.RequiredGems ? "Clear" : "Not Enough";
            timeText.text = FormatTime(GameManager.Instance.Timer);
            timeCheck.text = GameManager.Instance.Timer >= clearInfo.ClearTime ? "Clear" : "Not Enough";

            nextStageButton.gameObject.SetActive(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1);

            GameManager.Instance.SetStageResult(new GameResult(
                clearInfo.StageName, GameManager.Instance.Timer, clearInfo.Score
                ));
        }

        /// <summary>
        /// 다음 스테이지로 이동
        /// </summary>
        public void NextStage()
        {
            Close();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            return $"{minutes:00}:{seconds:00}";
        }
    }
}