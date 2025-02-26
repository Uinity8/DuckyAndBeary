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
        //플레이어 위치 확인을 위한 PlayerController 배열
        PlayerController[] players;

        Camera mainCamera;
        PixelPerfectCamera pixelPerfectCamera;

        enum GizmoLayer
        {
            카메라_이동_범위,
            카메라_시작_영역
        }
        //에디터 상에서 표현될 Gizmo 바꾸기
        [Header("Gizmo 레이어 바꾸기")]
        [SerializeField] GizmoLayer gizmoLayer;

        [Header("카메라 이동 가능 범위")]
        [SerializeField] Rect boundary;
        [SerializeField] Color boundaryColor;

        [Header("시작시 카메라 위치, 크기, 지속 시간")]
        [SerializeField] Rect startCameraArea;
        [SerializeField] private float duration;
        [SerializeField] Color startAreaColor;
        //설정한 범위를 통해 시작 위치와 크기를 계산
        private float startCameraSize;
        private Vector3 cameraCenter;
        private Vector3 readyPos;
        private bool isStarted = true;
        private float t = 0f;

        [Header("카메라 줌 인&아웃 조절")]
        [SerializeField] float cameraMargin;
        [SerializeField] float minValue;
        [SerializeField] float zoomSpeed = 2f; // 변환 속도
        private float targetOrthographicSize; // 목표 orthographicSize

        private const float cameraZPosition = -10f;
        private const float widthToHalfHeight = 1 / 3f;
        private const float halfHeight = 1 / 2f;

        [Header("배경 이미지 크기 조절")]
        [SerializeField] float imageRatio;
        Transform backgroundImage;

        //[SerializeField] float testValue;
        //float centerWeight;

        void Start()
        {
            players = FindObjectsOfType<PlayerController>();
            mainCamera = GetComponent<Camera>();
            pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
            readyPos = CalculatePlayersCenter();
            backgroundImage = transform.GetChild(0);

            // 초기 카메라 설정
            pixelPerfectCamera.CorrectCinemachineOrthoSize(0);
            cameraCenter = new Vector3(startCameraArea.x + startCameraArea.width / 2, startCameraArea.y + startCameraArea.height / 2);
            startCameraSize = startCameraArea.width * widthToHalfHeight - 2f;
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

            BackgroundImageControl();
        }

        private void HandleStartCamera()
        {
            t += Time.deltaTime;
            //시작시 카메라가 맵 중앙에서 플레이어를 향해 이동
            //맵 전체를 비추고 이후 플레이어를 향해 줌 인
            readyPos = CalculatePlayersCenter();
            MoveCamera(Vector3.Lerp(cameraCenter, readyPos, t / duration));
            mainCamera.orthographicSize = Mathf.Lerp(startCameraSize, minValue, t / duration);

            if (t / duration >= 1f)
            {
                t = 0f;
                //duration이 지나면 HandleUpdateCamera()를 실행
                isStarted = false;
            }
        }

        private void HandleUpdateCamera()
        {
            //플레이어들 간의 중점을 계산
            Vector2 middlePoint = CalculatePlayersCenter();
            //해당 중점으로 카메라를 이동(이동 제한 영역에 따라 제한)
            MoveCamera(middlePoint);
            //거리 값에 따라 카메라의 Size 값 조절
            ZoomInOrOut(CalculateMaxPlayerDistance(out bool isOnX), isOnX);
        }

        void OnDrawGizmosSelected()
        {
            switch (gizmoLayer)
            {
                case GizmoLayer.카메라_이동_범위:
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

                case GizmoLayer.카메라_시작_영역:
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
            //지정된 제한 영역에 따라 카메라 이동 범위를 제한
            Vector3 targetPos = new Vector3(
                Mathf.Clamp(middlePoint.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(middlePoint.y, boundary.yMin, boundary.yMax),
                cameraZPosition
            );

            transform.position = targetPos;
        }

        Vector2 CalculatePlayersCenter()
        {
            //플레이어들의 중점을 계산
            Vector2 center = players.Aggregate(Vector2.zero, (sum, player) => sum + (Vector2)player.transform.position);


            //centerWeight = testValue * Mathf.Pow(mainCamera.orthographicSize - minValue,2);
            //center += (Vector2)cameraCenter * centerWeight;
            //return center / (players.Length + centerWeight);
            
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
                        // 벡터를 사용하여 각도를 구함
                        Vector2 direction = players[j].transform.position - players[i].transform.position;
                        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 라디안에서 도로 변환
                    }
                }
            }
            //플레이어 간 거리에서 x 성분과 y 성분을 각각 구함
            float distanceX = Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad) * maxDistance);
            float distanceY = Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad) * maxDistance);

            // 화면 해상도에 따른 비율을 계산
            float ratio = (float)pixelPerfectCamera.refResolutionY / pixelPerfectCamera.refResolutionY;
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
                value = Mathf.Max(0, distance - (minValue - cameraMargin * 2)) * widthToHalfHeight;
            }
            else
            {
                value = Mathf.Max(0, distance - (minValue - cameraMargin * 2)) * halfHeight;
            }

            targetOrthographicSize = minValue + value;

            // 매 프레임마다 카메라 크기를 부드럽게 변화
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        }

        void BackgroundImageControl()
        {
            backgroundImage.localScale = Vector3.one * (mainCamera.orthographicSize / imageRatio);
        }
    }
}