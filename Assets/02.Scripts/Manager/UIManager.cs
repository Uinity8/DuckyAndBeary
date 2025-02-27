using UnityEngine;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;

        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<UIManager>();
                    if (_instance == null)
                    {
                        var managerObj = new GameObject("UIManager");
                        _instance = managerObj.AddComponent<UIManager>();
                    }
                }

                return _instance;
            }
        }

        private UIPopupManager popupManager;

        private void Awake()
        {
            popupManager = new UIPopupManager();
        }

        // 팝업 상태 제어는 UIPopupManager에 위임
        public void ShowPopup(string popupName) => popupManager.ShowPopup(popupName);

        public void ClosePopup(string popupName) => popupManager.ClosePopup(popupName);

        public void TogglePopup(string popupName) => popupManager.TogglePopup(popupName);
    }
}