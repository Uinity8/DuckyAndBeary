using UnityEngine;

namespace UI.Popup
{
    public abstract class UIPopup : MonoBehaviour
    {

        [SerializeField] public AudioClip buttonClick;
        [SerializeField] protected AudioClip clearClip;

        private void Awake()
        {
            LoadAudio();
        }

        private void LoadAudio()
        {
            buttonClick = Resources.Load<AudioClip>("_PopupButton");
            if (buttonClick == null)
            {
                Debug.LogError("클릭 사운드가 없습니다.");
            }
            clearClip = Resources.Load<AudioClip>("_ClearSound");
            if (clearClip == null)
            {
                Debug.LogError("클리어 사운드가 없습니다.");
            }
        }

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
            SoundManager.PlayClip(buttonClick);
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