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
        int stageClearLevel;
        [SerializeField] GameObject[] stars;
        [SerializeField] GameObject[] gemNumCheck;
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] GameObject[] timeCheck;

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
            gemNumCheck[0].SetActive(clearInfo.Score >= clearInfo.RequiredGems);
            gemNumCheck[1].SetActive(!gemNumCheck[0].activeSelf);
            timeText.text = FormatTime(GameManager.Instance.Timer);
            timeCheck[0].SetActive(GameManager.Instance.Timer >= clearInfo.ClearTime);
            timeCheck[1].SetActive(!timeCheck[0].activeSelf);

            ClearStarCheck();

            nextStageButton.gameObject.SetActive(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1);

            GameManager.Instance.SetStageResult(new GameResult(
                clearInfo.StageName, GameManager.Instance.Timer, clearInfo.Score
                ));
        }

        void ClearStarCheck()
        {
            if (clearInfo.IsCleared)
                stageClearLevel++;
            if (clearInfo.Score >= clearInfo.RequiredGems)
                stageClearLevel++;
            if (GameManager.Instance.Timer >= clearInfo.ClearTime)
                stageClearLevel++;

            for(int i = 0; i < stageClearLevel; i++)
            {
                stars[i].SetActive(true);
            }
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