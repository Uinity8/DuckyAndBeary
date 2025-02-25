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
        [SerializeField] Color boundaryColor;

        [Header("카메라 줌 인&아웃 조절")]
        [SerializeField] float cameraMargin;
        [SerializeField] float minValue;

        [Header("시작시 카메라 위치, 크기, 지속 시간")]
        [SerializeField] Rect startCameraArea;
        [SerializeField] private float duration;
        [SerializeField] Color startAreaColor;

        [Header("Gizmo 레이어 바꾸기 [1] 카메라 이동 범위 [2] 카메라 시작 영역")]
        [SerializeField] int gizmoLayer;

        private float startCameraSize;
        private Vector3 startCameraPos;
        private Vector3 readyPos;
        private bool isStarted = true;
        private float t = 0f;

        private const float cameraZPosition = -10f;

        void Start()
        {
            players = FindObjectsOfType<PlayerController>();
            mainCamera = GetComponent<Camera>();
            pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
            readyPos = CalculatePlayersCenter();

            // 초기 카메라 설정
            pixelPerfectCamera.CorrectCinemachineOrthoSize(0);
            startCameraPos = new Vector3(startCameraArea.x + startCameraArea.width / 2, startCameraArea.y + startCameraArea.height / 2);
            startCameraSize = startCameraArea.width/3f;
        }

        private void Update()
        {
            if (isStarted)
            {
                HandleStartCamera();
            }
            else
            {
                HandleUpdateCamera();
            }
        }

        private void HandleStartCamera()
        {
            t += Time.deltaTime;
            readyPos = CalculatePlayersCenter();
            MoveCamera(Vector3.Lerp(startCameraPos, readyPos, t / duration));
            mainCamera.orthographicSize = Mathf.Lerp(startCameraSize, minValue, t / duration);

            if (t / duration >= 1f)
            {
                t = 0f;
                isStarted = false;
            }
        }

        private void HandleUpdateCamera()
        {
            Vector2 middlePoint = CalculatePlayersCenter();
            MoveCamera(middlePoint);
            ZoomInOrOut(CalculateMaxPlayerDistance(out bool isOnX), isOnX);
        }

        void OnDrawGizmosSelected()
        {
            switch(gizmoLayer)
            {
                case 1:
                    if (boundary == default)
                    {
                        Debug.LogError("Camera Boundary가 설정되지 않았습니다.");
                        return;
                    }

                    Gizmos.color = boundaryColor;
                    Vector3 center = new Vector3(boundary.x + boundary.width / 2, boundary.y + boundary.height / 2);
                    Vector3 size = new Vector3(boundary.width, boundary.height);
                    Gizmos.DrawCube(center, size);
                    break;

                case 2:
                    if (startCameraArea == default)
                    {
                        Debug.LogError("Camera Start Area가 설정되지 않았습니다.");
                        return;
                    }
                    Gizmos.color = startAreaColor;
                    center = new Vector3(startCameraArea.x + startCameraArea.width / 2, startCameraArea.y + startCameraArea.height / 2);
                    size = new Vector3(startCameraArea.width, startCameraArea.height);
                    Gizmos.DrawCube(center, size);
                    break;
            }            
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
                value = Mathf.Max(0, distance - (minValue - cameraMargin * 2)) / 3f;
            }
            else
            {
                value = Mathf.Max(0, distance - (minValue - cameraMargin * 2)) / 2f;
            }

            mainCamera.orthographicSize = minValue + value;
        }
    }
}