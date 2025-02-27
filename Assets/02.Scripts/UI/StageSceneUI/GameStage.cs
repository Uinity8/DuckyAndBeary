using UnityEngine;

namespace Scripts.UI.StageSceneUI
{
    public class GameStage : MonoBehaviour

    {
        //스테이지 정보값
        public string StageName;      // 스테이지 이름 또는 고유 키
        public int StageIndex;        // 스테이지의 고유 인덱스
        public bool IsCleared;        // 스테이지 클리어 여부
        public int RequiredGems;      // 목표 Gem 수
        public float ClearTime;       // 목표 클리어 시간
        public int Score;             // 스테이지 점수


        public GameStage(string stageName, int stageIndex, int requiredGems, float clearTime)
        {
            this.StageName = stageName;
            this.StageIndex = stageIndex;
            IsCleared = false;
            this.RequiredGems = requiredGems;
            this.ClearTime = clearTime;
            Score = 0; // 초기 점수는 0으로 시작
        }

    }
}
