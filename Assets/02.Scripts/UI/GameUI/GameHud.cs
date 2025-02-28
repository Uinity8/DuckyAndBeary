using Manager;
using Scripts.UI.StageSceneUI;
using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameHud : UIHud
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
            timerText.text = GameManager.Instance.Timer.FormatTime();
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
        
        
    }
}