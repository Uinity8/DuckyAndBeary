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
        [SerializeField] TextMeshProUGUI limitTime;
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] GameObject[] timeCheck;

        [SerializeField] Button nextStageButton;

        public override void Initialize()
        {
            base.Initialize();
            //게임 클리어 정보 불러오기
            clearInfo = GameManager.Instance.GetCurrentStageInfo();
            ShowStageResult();

        }

        void ShowStageResult()
        {
            //clearInfo 값에 따라 UI 설정
            limitTime.text = clearInfo.ClearTime.FormatTime();

            gemNumCheck[0].SetActive(GameManager.Instance.NumberOfGem >= clearInfo.RequiredGems);
            gemNumCheck[1].SetActive(!gemNumCheck[0].activeSelf);
            timeText.text = GameManager.Instance.Timer.FormatTime();
            timeCheck[0].SetActive(GameManager.Instance.Timer <= clearInfo.ClearTime);
            timeCheck[1].SetActive(!timeCheck[0].activeSelf);

            ClearStarCheck();

            nextStageButton.gameObject.SetActive(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1);

            if (GetNextStageName() != null)
                //다음 스테이지 Unlock
                GameManager.Instance.SetStageResult(new GameResult(GetNextStageName(),0, 0, StageStatus.Unlocked));

        }

        void ClearStarCheck()
        {
            stageClearLevel = 1;
            stageClearLevel += gemNumCheck[0].activeSelf ? 1 : 0;
            stageClearLevel += timeCheck[0].activeSelf ? 1 : 0;

            for (int i = 0; i < stageClearLevel; i++)
            {
                stars[i].SetActive(true);
            }

            Debug.Log("별 개수 : " + stageClearLevel);
        }

        /// <summary>
        /// 다음 스테이지로 이동
        /// </summary>
        public void NextStage()
        {
            Close();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public string GetNextStageName()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            Debug.Log($"currentSceneIndex : {currentSceneIndex}");

            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
            {
                return "Stage" + (nextSceneIndex - 1).ToString();

            }
            else
            {
                return null;
            }

        }
    }
}