using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

namespace Entity
{
    public class CameraController : MonoBehaviour
    {
        PlayerController[] players;
        Camera mainCamera;
        PixelPerfectCamera pixelPerfectCamera;

        [Header("카메라 이동 가능 범위")]
        [SerializeField] Rect boundary;
        [SerializeField] Color gizmoColor;

        [Header("카메라 줌 인&아웃 조절")]
        [SerializeField] float cameraMargin;
        [SerializeField] float minValue;

        private const float cameraZPosition = -10f;

        [SerializeField] float startCameraSize;
        [SerializeField] Vector3 startCameraPos;
        Vector3 readyPos = new Vector3(0f, 1f, -15f);
        float readySize = 6;

        float t = 0f;

        bool isOnX;

        void Start()
        {
            players = FindObjectsOfType<PlayerController>();
            mainCamera = GetComponent<Camera>();
            pixelPerfectCamera = GetComponent<PixelPerfectCamera>();

            //mainCamera.orthographicSize = startCameraSize;
            //mainCamera.transform.position = startCameraPos;
        }

        private void Update()
        {
            //StartAnimation();
            UpdateCamera();

            //Invoke("UpdateCamera", 3f);
        }

        void StartAnimation()
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startCameraPos, readyPos, t / 1f);
            mainCamera.orthographicSize = Mathf.Lerp(startCameraSize, readySize, t / 1f);

        }

        void UpdateCamera()
        {
            Vector2 middlePoint = CalculatePlayersCenter();
            MoveCamera(middlePoint);
            ZoomInOrOut(CalculateMaxPlayerDistance(out isOnX),isOnX);

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

            transform.position = targetPos;
        }

        Vector2 CalculatePlayersCenter()
        {
            Vector2 center = players.Aggregate(Vector2.zero, (sum, player) => sum + (Vector2)player.transform.position);
            return center / players.Length;
        }

        float CalculateMaxPlayerDistance(out bool isOnX)
        {
            float maxDistance = 0f;
            float angle = 0f;

            for (int i = 0; i < players.Length; i++)
            {
                for (int j = i + 1; j < players.Length; j++)
                {
                    float tempDistance = Vector2.Distance(players[i].transform.position, players[j].transform.position);

                    if (tempDistance > maxDistance)
                    {
                        maxDistance = tempDistance;
                        // 벡터를 사용하여 각도를 구합니다.
                        Vector2 direction = players[j].transform.position - players[i].transform.position;
                        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 라디안에서 도로 변환
                    }
                }
            }

            float distanceX = Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad) * maxDistance);
            float distanceY = Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad) * maxDistance);

            // 비율을 계산합니다.
            float ratio = (float) pixelPerfectCamera.refResolutionY / pixelPerfectCamera.refResolutionY;
            float tanValue = Mathf.Tan(ratio);

            if (distanceX > distanceY * tanValue)
            {
                isOnX = true;
                return distanceX;
            }
            else
            {
                isOnX = false;
                return distanceY;
            }
        }

        void ZoomInOrOut(float distance, bool isOnX)
        {
            pixelPerfectCamera.CorrectCinemachineOrthoSize(0);

            float value;

            if (isOnX)
            {
                value = Mathf.Max(0, distance- (minValue-cameraMargin * 2)) / 3f;
            }
            else
            {
                value = Mathf.Max(0, distance - (minValue - cameraMargin * 2));
            }

            mainCamera.orthographicSize = minValue + value;
        }
    }
}