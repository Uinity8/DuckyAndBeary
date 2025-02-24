using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //PlayerController���� ã�� �迭�� ����
    PlayerController[] players;
    Camera mainCamera;

    [Header("ī�޶� �̵� ���� ����")]
    [SerializeField] Rect boundary;
    [SerializeField] Color gizmoColor;

    [Header("ī�޶� �� ��&�ƿ� ����")]
    [SerializeField] float distanceWeight;
    [SerializeField] float minValue;

    void Start()
    {
        //���۽� PlayerController�� ���� ������Ʈ�� ã�� �迭�� ����
        players = FindObjectsOfType<PlayerController>();
        //Main Camera ã��
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MoveCamera(CalculateMiddle());
        ZoomInOrOut(CalculateDistance());
    }

    //ī�޶� �̵� ���� ���� �׸���
    void OnDrawGizmosSelected()
    {
        if (boundary == null) return;

        Gizmos.color = gizmoColor;

        Vector3 center = new Vector3(boundary.x + boundary.width / 2, boundary.y + boundary.height / 2);
        Vector3 size = new Vector3(boundary.width, boundary.height);

        Gizmos.DrawCube(center, size);
    }

    void MoveCamera(Vector2 middlePoint)
    {
        //ī�޶� �̵��� �ִ� ������ ���
        float maxX = boundary.width / 2f;
        float minX = -boundary.width / 2f;
        float maxY = boundary.height / 2f;
        float minY = -boundary.height / 2f;

        Vector3 targetPos = Vector3.zero;
        //Clamp�� ���� ī�޶� �̵� ������ ����
        targetPos = middlePoint;
        targetPos.x = Mathf.Clamp(middlePoint.x, minX, maxX);
        targetPos.y = Mathf.Clamp(middlePoint.y, minY, maxY);
        targetPos = Vector2.Lerp(targetPos, transform.position, 0f);
        //ī�޶��� z ���� -10���� ����(�������� ������ 0�� �Ǿ� ī�޶� ������Ʈ�� ������ ����)
        targetPos.z = -10f;
        transform.position = targetPos;
    }

    Vector2 CalculateMiddle()
    {
        //��ü �÷��̾� ������Ʈ�� ������ ����
        float sumX = 0;
        float sumY = 0;

        foreach (PlayerController player in players)
        {
            sumX += player.transform.position.x;
            sumY += player.transform.position.y;
        }

        return new Vector2(sumX / players.Length, sumY / players.Length);
    }

    //�÷��̾� ������Ʈ ���� �ִ� �Ÿ��� ����
    float CalculateDistance()
    {
        float maxDistance = 0;

        for (int i = 0; i < players.Length; i++)
        {
            for (int j = i + 1; j < players.Length; j++)
            {
                float tempDistance = (players[i].transform.position - players[j].transform.position).magnitude;

                if (tempDistance > maxDistance)
                    maxDistance = tempDistance;
            }
        }

        return maxDistance;
    }
    //�Ÿ� ���� ���� ī�޶� �� �� or �ƿ��� ����
    //����ġ�� �ּ� ���� ������ �� ����
    void ZoomInOrOut(float distance)
    {
        mainCamera.orthographicSize = minValue + distanceWeight * MathF.Sqrt(distance);
    }
}
