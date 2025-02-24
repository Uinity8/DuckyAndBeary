using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController[] players;
    Camera camera;

    [Header("카메라 이동 가능 범위")]
    [SerializeField] Rect boundary;
    [SerializeField] Color gizmoColor;

    [Header("카메라 줌 인&아웃 조절")]
    [SerializeField] float distanceWeight;
    [SerializeField] float minValue;

    private const float cameraZPosition = -10f;

    void Start()
    {
        players = FindObjectsOfType<PlayerController>();
        camera = Camera.main;
    }

    private void Update()
    {
        Vector2 middlePoint = CalculatePlayersCenter();
        MoveCamera(middlePoint);
        ZoomInOrOut(CalculateMaxPlayerDistance());
    }

    void OnDrawGizmosSelected()
    {
        if (boundary == default) return;

        Gizmos.color = gizmoColor;
        Vector3 center = new Vector3(boundary.x + boundary.width / 2, boundary.y + boundary.height / 2);
        Vector3 size = new Vector3(boundary.width, boundary.height);
        Gizmos.DrawCube(center, size);
    }

    void MoveCamera(Vector2 middlePoint)
    {
        Vector3 targetPos = new Vector3(
            Mathf.Clamp(middlePoint.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(middlePoint.y, boundary.yMin, boundary.yMax),
            cameraZPosition
        );

        transform.position = Vector2.Lerp(targetPos, transform.position, 0f);
    }

    Vector2 CalculatePlayersCenter()
    {
        Vector2 center = players.Aggregate(Vector2.zero, (sum, player) => sum + (Vector2)player.transform.position);
        return center / players.Length;
    }

    float CalculateMaxPlayerDistance()
    {
        float maxDistance = 0;

        for (int i = 0; i < players.Length; i++)
        {
            for (int j = i + 1; j < players.Length; j++)
            {
                float tempDistance = Vector2.Distance(players[i].transform.position, players[j].transform.position);
                maxDistance = Mathf.Max(maxDistance, tempDistance);
            }
        }

        return maxDistance;
    }

    void ZoomInOrOut(float distance)
    {
        camera.orthographicSize = minValue + distanceWeight * Mathf.Sqrt(distance);
    }
}