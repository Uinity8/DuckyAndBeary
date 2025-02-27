using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StageSceneButtonUI : MonoBehaviour
{
    [Header("GetButton")]
    [SerializeField] private Button mainButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [Header("MoveStageSelect")]
    [SerializeField] private RectTransform stage;
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveDuration;

    private Vector2 targetPosition;

    private void Awake()
    {
        targetPosition = stage.anchoredPosition;

        mainButton.onClick.AddListener(LoadMainScene);
        prevButton.onClick.AddListener(() => MoveStage(true));
        nextButton.onClick.AddListener(() => MoveStage(false));
    }

    private void LoadMainScene()
    {    
        SceneManager.LoadScene("StartScene");
    }

    private void MoveStage(bool isLeft)
    {
        int direction = isLeft ? -1 : 1;

        if (!isLeft && targetPosition.x > 50) return;
        if (isLeft && targetPosition.x < -50) return;

        targetPosition += new Vector2(direction * moveDistance, 0);

        if (targetPosition != null)
        {
            stage.DOAnchorPos(targetPosition, moveDuration).SetEase(Ease.OutQuad);
        }
    }

    private void OnDestroy()
    {
        if (stage != null)
        {
            stage.DOKill();
        }
        DOTween.Kill(this);
    }
}
