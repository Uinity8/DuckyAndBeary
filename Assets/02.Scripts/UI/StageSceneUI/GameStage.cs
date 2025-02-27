using UnityEngine;

namespace Scripts.UI.StageSceneUI
{
    public class GameStage : MonoBehaviour

    {
        //스테이지 정보값
        public string StageName;      // 스테이지 이름 또는 고유 키
        public int StageIndex;        // 스테이지의 고유 인덱스
        public int RequiredGems;      // 목표 Gem 수
        public float ClearTime;       // 목표 클리어 시간


        public GameStage(string stageName, int stageIndex, int requiredGems, float clearTime)
        {
            this.StageName = stageName;
            this.StageIndex = stageIndex;
            this.RequiredGems = requiredGems;
            this.ClearTime = clearTime;
        }

    }
}
