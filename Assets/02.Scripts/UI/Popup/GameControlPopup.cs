using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Popup
{
    /// <summary>
    /// Time.timeScale 및 공통 동작을 처리하는 팝업의 기본 클래스
    /// </summary>
    public abstract class GameControlPopup : UIPopup
    {
        public override void Initialize()
        {
            base.Initialize();
            Time.timeScale = 0; // 게임 정지
        }

        public override void OnClose()
        {
            base.OnClose();
            Time.timeScale = 1; // 게임 재개
        }

        /// <summary>
        /// 현재 씬을 다시 시작
        /// </summary>
        public virtual void Restart()
        {
            Close();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// 스테이지 선택 화면으로 이동
        /// </summary>
        public virtual void ExitToStageSelect()
        {
            Close();
            SceneManager.LoadScene("StageSelect");
        }
    }
}