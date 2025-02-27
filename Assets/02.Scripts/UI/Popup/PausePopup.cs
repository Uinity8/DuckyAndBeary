using Manager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Popup
{
    public class PausePopup : GameControlPopup
    {

        public virtual void ShowSettingPopup()
        {
            SoundManager.PlayClip(buttonClick);
            UIManager.Instance.ShowPopup("SettingPopup");
        }
    }
}
