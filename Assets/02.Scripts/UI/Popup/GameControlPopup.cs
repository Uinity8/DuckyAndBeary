using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Popup
{
    /// <summary>
    /// Time.timeScale 및 공통 동작을 처리하는 팝업의 기본 클래스
    /// </summary>
    public abstract class GameControlPopup : UIPopup
    {
        //SerializeField는 인스펙터 창에서 Awake 이후 클립이 잘 들어갔나 확인하기 위해서 작성했습니다.

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
            //디버그 로그를 남겨보면 클릭 소리가 호출이 되긴 하지만, 바로 씬을 로드해서 소리가 들리지 않게 됩니다. 순서를 뒤로 바꿔봐도 똑같더라고요.
            SoundManager.PlayClip(buttonClick);
            Close();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// 스테이지 선택 화면으로 이동
        /// </summary>
        public virtual void ExitToStageSelect()
        {
            SoundManager.PlayClip(buttonClick);
            Close();
            SceneManager.LoadScene("StageSelect");
        }
    }
}