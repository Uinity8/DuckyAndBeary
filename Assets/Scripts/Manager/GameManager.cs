using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] float timer;
        public float Timer => timer;

        [SerializeField] int score;
        public int Score { get { return score; } }
        private const int NumForClear = 2;
        private int OpenExitDorCount = 0;


        static public GameManager instance;
        SignalManager signalManager;

        public GameStage[] stageInfo = new GameStage[0];

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    Instantiate(go);
                    instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }

                return instance;
            }
        }

        private void Awake()
        {
            signalManager = SignalManager.Instance;
        }

        private void Start()
        {
            signalManager.ConnectSignal(SignalKey.OpenDoor, OnOpenExitDoor);
            signalManager.ConnectSignal(SignalKey.CloseDoor, OnCloseExitDoor);
        }

        private void Update()
        {
            timer += Time.deltaTime;
        }

        public void AddScore(int value)
        {
            score += value;
        }

        public void Clear(GameStage stageClear)
        {
            stageInfo[stageClear.StageIndex] = stageClear;
            Debug.Log($"값 저장 완료, 저장된 값:{stageInfo[stageClear.StageIndex].StageIndex}");
        }

        public void OnDestroy()
        {
            signalManager.DisconnectSignal(SignalKey.OpenDoor, OnOpenExitDoor);
            signalManager.DisconnectSignal(SignalKey.CloseDoor, OnCloseExitDoor);
        }

        void OnOpenExitDoor(object sender)
        {
            OpenExitDorCount++;
            if (OpenExitDorCount >= NumForClear)
            {
                SignalManager.Instance.EmitSignal(SignalKey.GameClear);
            }
        }

        void OnCloseExitDoor(object sender)
        {
            OpenExitDorCount--;
        }

        //데이터 저장
    }
}