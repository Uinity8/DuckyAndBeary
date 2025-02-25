using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSceneButtonUI : MonoBehaviour
{
    [Header("GetButton")]
    [SerializeField] private Button maintButton;
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

        maintButton.onClick.AddListener(LoadMainScene);
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
