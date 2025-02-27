public enum StageStatus
{
    Locked,
    Unlocked,
    Cleared,
    PerfectlyCleared
}

public class GameResult
{
    public string stageName;
    public float passedTime;
    public int score;
    public StageStatus stageStatus;

    public GameResult(string stageName, float passedTime, int score, StageStatus stageStatus)
    {
        this.stageName = stageName;
        this.passedTime = passedTime;
        this.score = score;
        this.stageStatus = stageStatus;
    }
}
