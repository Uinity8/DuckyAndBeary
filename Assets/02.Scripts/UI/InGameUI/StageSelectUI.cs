using UnityEngine;

public class StageSelectUI : MonoBehaviour
{
    public void LoadStage(int stageIndex)
    {
        PlayerPrefs.SetInt("SelectedStage", stageIndex);
        UIManager.Instance.LoadScene("GameScene");
    }

    public void GoBack()
    {
        UIManager.Instance.LoadScene("TitleScene");
    }
}