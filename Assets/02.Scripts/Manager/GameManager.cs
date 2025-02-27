using System.Collections.Generic;
using UnityEngine;
using Scripts.UI.StageSceneUI;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        SignalManager signalManager;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();

                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
                        //DontDestroyOnLoad(go);
                    }
                }
                

                return _instance;
            }
        }
        
        float timer;
        public float Timer => timer;
        private int numberOfGem;
        
        const int NumForClear = 2;
        int openExitDoorCount;


        private readonly Dictionary<string, GameStage> stageInfo = new Dictionary<string, GameStage>();
        static private readonly Dictionary<string, GameResult> stageResultInfo = new Dictionary<string, GameResult>();

        private void Awake()
        {
            signalManager = SignalManager.Instance;
            
            // 스테이지 데이터를 초기화
            InitializeStages();
            Debug.Log("스테이지 정보 초기화");
        }
        
        private void InitializeStages()
        {
            // 딕셔너리로 스테이지 초기화 (씬 이름을 키로 사용)
            stageInfo.Add("Stage1", new GameStage("Stage1", 0, 6, 130f));
            stageInfo.Add("Stage2", new GameStage("Stage2", 1, 6, 150f));
            stageInfo.Add("Stage3", new GameStage("Stage3", 2, 6, 150f));
        }

        private void Start()
        {
            timer = 0;
            numberOfGem = 0;
            
            signalManager.ConnectSignal(SignalKey.OpenDoor, OnOpenExitDoor);
            signalManager.ConnectSignal(SignalKey.CloseDoor, OnCloseExitDoor);
        } 

        private void Update()
        {
            timer += Time.deltaTime;
        }

        public void AddScore(int value)
        {
            numberOfGem += value;
        }
        
        public GameStage GetCurrentStageInfo()
        {
            // 현재 씬의 이름을 키로 스테이지 데이터 반환
            string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (stageInfo.TryGetValue(currentSceneName, out GameStage currentStage))
            {
                return currentStage;
            }
            Debug.LogError($"씬 이름 {currentSceneName}에 해당하는 스테이지 정보를 찾을 수 없습니다!");
            return null;
        }
        
        private GameStage ClearStage(GameStage currentStage, int scoreAchieved)
        {
            currentStage.IsCleared = true; // 클리어 여부 업데이트
            currentStage.Score = scoreAchieved; // 점수 업데이트

            Debug.Log($"{currentStage.StageName} 완료! 점수: {currentStage.Score}");
            
            return currentStage;
        }

        void OnOpenExitDoor(object sender)
        {
            openExitDoorCount++;
            if (openExitDoorCount >= NumForClear)
            {
                GameStage clearStage = ClearStage(GetCurrentStageInfo(), numberOfGem);
                SignalManager.Instance.EmitSignal(SignalKey.GameClear, clearStage);
            }
        }
        
        void OnCloseExitDoor(object sender)
        {
            openExitDoorCount--;
        }

        public void SetStageResult(GameResult result)
        {
            GameResult tempResult;

            if(stageResultInfo.TryGetValue(result.stageName, out tempResult))
            {
                if(tempResult.passedTime < result.passedTime || tempResult.score < result.score)
                {
                    stageResultInfo[result.stageName] = result;
                    Debug.Log("결과 갱신");
                }

            }
            else
            {
                //처음 결과를 저장할 때
                stageResultInfo.Add(result.stageName, result);
                Debug.Log("결과 저장");
                Debug.Log($"{stageResultInfo[result.stageName].stageName} {stageResultInfo[result.stageName].passedTime}");
            }


        }
        
     }

    public class GameResult
    {
        public string stageName;
        public float passedTime;
        public int score;

        public GameResult(string stageName, float passedTime, int score )
        {
            this.stageName = stageName;
            this.passedTime = passedTime;
            this.score = score;
        }
    }
}