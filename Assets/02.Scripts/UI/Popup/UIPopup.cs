using UnityEngine;

namespace UI.Popup
{
    public abstract class UIPopup : MonoBehaviour
    {
        /// <summary>
        /// 팝업이 열릴 때 초기화 로직 처리. 서브클래스에서 구현.
        /// </summary>
        public virtual void Initialize()
        {
            Debug.Log($"[{gameObject.name}] - Initialize called.");
        }

        /// <summary>
        /// 팝업이 닫힐 때 정리 작업 처리. 서브클래스에서 구현.
        /// </summary>
        public virtual void OnClose()
        {
            Debug.Log($"[{gameObject.name}] - OnClose called.");
        }

        /// <summary>
        /// 팝업 닫기 메서드
        /// </summary>
        public void Close()
        {
            OnClose();
            gameObject.SetActive(false);
        }
    }
}