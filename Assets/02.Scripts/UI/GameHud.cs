using Manager;
using Scripts.UI.StageSceneUI;
using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameHud : MonoBehaviour
    {
        [SerializeField]  TMP_Text timerText;
        [SerializeField] private AudioClip buttonClick; //SerializeField는 인스턴스에서 클립이 제대로 들어갔는지 확인하기 위해 작성했습니다.
        private bool audioPlayed; //오디오가 재생되어야 하는 상황인지(


        private void Start()
        {
            LoadAudio();
            SignalManager.Instance.ConnectSignal(SignalKey.GameOver, _=>ShowGameOverPopup());
            SignalManager.Instance.ConnectSignal(SignalKey.GameClear, _ => ShowResultPopup());
        }
        private void LoadAudio()
        {
            buttonClick = Resources.Load<AudioClip>("_PopupButton");
            if (buttonClick == null)
            {
                Debug.LogError("클릭 사운드가 없습니다.");
            }
        }

        void Update()
        {
            UpdateTimer();
        }

        void UpdateTimer()
        {
            timerText.text = FormatTime(GameManager.Instance.Timer);
        }

        public void TogglePause()
        {
            UIManager.Instance.TogglePopup("PausePopup");
        }
        
        void ShowGameOverPopup()
        {
            UIManager.Instance.ShowPopup("GameOverPopup");
        }
        void ShowResultPopup()
        {
            UIManager.Instance.ShowPopup("LevelCompletePopup");
        }
        

        public string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            return $"{minutes:00}:{seconds:00}";
        }
        
    }
}