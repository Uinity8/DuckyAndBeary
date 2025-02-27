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
        public int NumberOfGem => numberOfGem;
        
        const int NumForClear = 2;
        int openExitDoorCount;


        private readonly Dictionary<string, GameStage> stageInfo = new Dictionary<string, GameStage>();
        private static readonly Dictionary<string, GameResult> StageResultInfo = new Dictionary<string, GameResult>();

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
                int starCount = 1;
                starCount += numberOfGem >= currentStage.RequiredGems ? 1 : 0;
                starCount += Timer <= currentStage.ClearTime ? 1 : 0;

                StageStatus currentStageStatus = starCount == 3 ? StageStatus.PerfectlyCleared : StageStatus.Cleared;

                SetStageResult(new GameResult(currentStage.StageName, timer, starCount, currentStageStatus));
                
                return currentStage;
            }
            Debug.LogError($"씬 이름 {currentSceneName}에 해당하는 스테이지 정보를 찾을 수 없습니다!");
            return null;
        }

        void OnOpenExitDoor(object sender)
        {
            openExitDoorCount++;
            if (openExitDoorCount >= NumForClear)
            {
                SignalManager.Instance.EmitSignal(SignalKey.GameClear);
            }
        }
        
        void OnCloseExitDoor(object sender)
        {
            openExitDoorCount--;
        }

        public void SetStageResult(GameResult result)
        {
            if(StageResultInfo.TryGetValue(result.stageName, out GameResult tempResult))
            {
                if (tempResult.score >= result.score)
                {
                    return;
                }

                StageResultInfo[result.stageName] = result;
                Debug.Log("결과 갱신");

            }
            else
            {
                //처음 결과를 저장할 때
                StageResultInfo.Add(result.stageName, result);
                Debug.Log("결과 저장");
                Debug.Log($"{StageResultInfo[result.stageName].stageName} {StageResultInfo[result.stageName].passedTime}");
            }


        }

        public GameResult GetStageResult(string stageName)
        {
            if(StageResultInfo.TryGetValue(stageName, out GameResult tempResult))
            {
                return tempResult;
            }

            else
            {
                Debug.LogWarning(stageName + "is not found");
                return new GameResult(stageName,0,0,StageStatus.Locked);
            }
        }
        
     }
}