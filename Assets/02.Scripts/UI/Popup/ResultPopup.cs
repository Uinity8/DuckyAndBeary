using UnityEngine.SceneManagement;

namespace UI.Popup
{
    public class ResultPopup : GameControlPopup
    {
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