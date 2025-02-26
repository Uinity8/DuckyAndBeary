using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.InGameUI
{
    public class GameUI : MonoBehaviour
    {
        public GameObject pausePanel;
        public GameObject gameOverPanel;

        public GameObject gameClearPanel;
        public TMP_Text timerText;
        private float timeElapsed;
        private bool isGameOver;
        private int totalGem;
        ItemController[] Gems;

        [SerializeField] private float missionTime;
        [SerializeField] private TextMeshProUGUI clearNum;
        [SerializeField] private TextMeshProUGUI gemNum;
        [SerializeField] private TextMeshProUGUI timeNum;
        [SerializeField] private TextMeshProUGUI clearCheck;
        [SerializeField] private TextMeshProUGUI gemCheck;
        [SerializeField] private TextMeshProUGUI timeCheck;


        private SignalManager signalManager;

        private void Awake()
        {
            signalManager = SignalManager.Instance;
        }


        private void Start()
        {
            Gems = FindObjectsOfType<ItemController>();
            totalGem = Gems.Length;
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            gameClearPanel.SetActive(false);
            isGameOver = false;
            signalManager.ConnectSignal(SignalKey.GameOver, OnGameOver);
            signalManager.ConnectSignal(SignalKey.GameClear, OnGameClear);
        }

        void Update()
        {
            if (isGameOver) return;

            timeElapsed += Time.deltaTime;
            timerText.text = FormatTime(timeElapsed);
        }

        string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }


        public bool GemCheck(int collectedGem, int nomForGem)
        {
            gemNum.text = ($"{collectedGem}/{nomForGem}");
            if (collectedGem < nomForGem)
            {
                Debug.Log("너무 적습니다.");
                gemCheck.text = ($"Failed");
                return false;
            }
            else
            {
                Debug.Log("딱 적당히 모았습니다.");
                gemCheck.text = ($"Success");
                return true;
            }
        }

        public bool TimeCheck(float usedTime)
        {
            string textTime = usedTime.ToString("F2");
            timeNum.text = ($"{textTime}/{missionTime}");
            if (usedTime <= missionTime)
            {
                Debug.Log("적당히 빨랐습니다.");
                timeCheck.text = ($"Success");
                return true;
            }
            else
            {
                Debug.Log("느렸습니다.");
                timeCheck.text = ($"Failed");
                return false;
            }
        }

        public void TogglePause()
        {
            UIManager.Instance.PauseGame();
            bool isPaused = UIManager.Instance.IsPaused;
            pausePanel.SetActive(isPaused);
        }

        public void OnGameOver(object[] args)
        {
            gameOverPanel.SetActive(true);
            isGameOver = true;
        }

        public void OnGameClear(object sender)
        {
            
            //게임 매니저로 옮겨야힘
            // Debug.Log("GameClear 신호 받음");
            // string sceneName = SceneManager.GetActiveScene().name;
            // string num = sceneName.Replace("Stage", "");
            // Debug.Log($"{num}");
            // int n = int.Parse(num);
            //
            // bool gemClear = GemCheck(GameManager.Instance.Score, totalGem);
            // bool timeClear = TimeCheck(GameManager.Instance.PassedTIme);
            //
            // GameStage stageclear = new GameStage(n, true, 3);
            //
            // Debug.Log($"값 저장 완료, 저장된 값:{stageclear.StageIndex}");
            //

            gameClearPanel.SetActive(true);
            isGameOver = true;
        }

        public void Restart()
        {
            bool isPaused = UIManager.Instance.IsPaused;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            if (isPaused)
                UIManager.Instance.PauseGame();
        }

        public void ExitToStageSelect()
        {
            UIManager.Instance.LoadScene("StartScene");
        }

        private void OnDestroy()
        {
            signalManager.DisconnectSignal(SignalKey.GameOver, OnGameOver);
            signalManager.DisconnectSignal(SignalKey.GameClear, OnGameClear);
        }
    }
}