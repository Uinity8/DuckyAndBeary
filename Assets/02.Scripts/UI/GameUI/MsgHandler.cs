using Entity;
using TMPro;
using UnityEngine;

public class MsgHandler : MonoBehaviour
{
    private TextMeshProUGUI msgText;
    private int playerCount = 0; // 현재 트리거 안에 있는 플레이어 수
    private float fadeSpeed = 2.0f; // 알파값 변화 속도
    private float currentAlpha = 0f; // 현재 텍스트 알파 값

    private void Awake()
    {
        msgText = GetComponentInChildren<TextMeshProUGUI>();
        SetAlpha(0f); // 초기 상태에서 텍스트를 투명하게 설정
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>()) // 플레이어인지 확인
        {
            playerCount++; // 트리거에 들어온 플레이어 수 증가
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>()) // 플레이어인지 확인
        {
            // 플레이어가 하나 이상 있을 때, 알파값을 증가
            currentAlpha = Mathf.Min(1f, currentAlpha + fadeSpeed * Time.deltaTime);
            SetAlpha(currentAlpha);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>()) // 플레이어인지 확인
        {
            playerCount--; // 트리거에서 나간 플레이어 수 감소

            // 트리거 안에 플레이어가 없으면 페이드아웃 진행
            if (playerCount <= 0)
            {
                playerCount = 0; // 음수 값 방지
            }
        }
    }

    private void Update()
    {
        // 트리거 안에 플레이어가 없으면 알파값을 감소
        if (playerCount == 0)
        {
            currentAlpha = Mathf.Max(0f, currentAlpha - fadeSpeed * Time.deltaTime);
            SetAlpha(currentAlpha);
        }
    }

    private void SetAlpha(float alpha)
    {
        // 텍스트 알파값을 적용
        Color color = msgText.color;
        msgText.color = new Color(color.r, color.g, color.b, alpha);
    }
}