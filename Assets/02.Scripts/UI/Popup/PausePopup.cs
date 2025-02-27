using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Popup
{
    public class PausePopup : GameControlPopup
    {

        public virtual void ShowSettingPopup()
        {
            UIManager.Instance.ShowPopup("SettingPopup");
        }
    }
}
