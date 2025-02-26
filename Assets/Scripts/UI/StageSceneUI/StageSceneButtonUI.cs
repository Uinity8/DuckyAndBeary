using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StageSceneButtonUI : MonoBehaviour
{
    [FormerlySerializedAs("maintButton")]
    [Header("GetButton")]
    [SerializeField] private Button mainButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [Header("MoveStageSelect")]
    [SerializeField] private RectTransform stage;
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveDuration;
    [SerializeField] private float scaleFactor = 1.5f;

    private Vector2 targetPosition;
    private RectTransform[] stagePostions;

    private void Awake()
    {
        targetPosition = stage.anchoredPosition;
        stagePostions = stage.GetComponentsInChildren<RectTransform>();

        mainButton.onClick.AddListener(LoadMainScene);
        prevButton.onClick.AddListener(()=>MoveStage(true));
        nextButton.onClick.AddListener(() => MoveStage(false));
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    private void MoveStage(bool isLeft)
    {
        int direction = isLeft ? -1 : 1;

        targetPosition += new Vector2(direction * moveDistance, 0);

        stage.DOAnchorPos(targetPosition, moveDuration).SetEase(Ease.OutQuad);


    }

}
