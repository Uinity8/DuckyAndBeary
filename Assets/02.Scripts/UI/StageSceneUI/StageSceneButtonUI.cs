using DG.Tweening;
using Unity.VisualScripting;
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
    [SerializeField] private AudioClip buttonClip;

    private Vector2 targetPosition;

    private void Awake()
    {
        LoadAudio();
        targetPosition = stage.anchoredPosition;

        mainButton.onClick.AddListener(LoadMainScene);
        prevButton.onClick.AddListener(() => MoveStage(true));
        nextButton.onClick.AddListener(() => MoveStage(false));
    }
    private void LoadAudio()
    {
        buttonClip = Resources.Load<AudioClip>("_PopupButton");
        if (buttonClip == null)
        {
            Debug.LogError("클릭 사운드가 없습니다.");
        }
    }

    private void LoadMainScene()
    {    
        SceneManager.LoadScene("StartScene");
        SoundManager.PlayClip(buttonClip);
    }

    private void MoveStage(bool isLeft)
    {
        SoundManager.PlayClip(buttonClip);
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
