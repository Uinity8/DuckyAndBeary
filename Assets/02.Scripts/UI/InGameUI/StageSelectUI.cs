using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectUI : MonoBehaviour
{
    public void LoadStage(int stageIndex)
    {
        PlayerPrefs.SetInt("SelectedStage", stageIndex);
        SceneManager.LoadScene("GameScene");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("TitleScene");
    }
}