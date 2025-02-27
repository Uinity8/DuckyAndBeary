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


        private void Start()
        {
            SignalManager.Instance.ConnectSignal(SignalKey.GameOver, _=>ShowGameOverPopup());
            SignalManager.Instance.ConnectSignal(SignalKey.GameClear, _ => ShowResultPopup());
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