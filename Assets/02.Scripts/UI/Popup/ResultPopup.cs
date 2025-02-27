using Manager;
using Scripts.UI.StageSceneUI;
//using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

namespace UI.Popup
{
    public class ResultPopup : GameControlPopup
    {
        GameStage clearInfo;

        public override void Initialize()
        {
            base.Initialize();
            clearInfo = GameManager.Instance.GetCurrentStageInfo();
            Debug.Log($"{clearInfo.StageName} / {clearInfo.Score} / {clearInfo.ClearTime}");
        }

        /// <summary>
        /// 다음 스테이지로 이동
        /// </summary>
        public void NextStage()
        {
            Close();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}