using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //PlayerController들을 찾아 배열로 저장
    PlayerController[] players;
    Camera camera;

    [Header("카메라 이동 가능 범위")]
    [SerializeField] Rect boundary;
    [SerializeField] Color gizmoColor;

    [Header("카메라 줌 인&아웃 조절")]
    [SerializeField] float distanceWeight;
    [SerializeField] float minValue;

    void Start()
    {
        //시작시 PlayerController를 가진 오브젝트를 찾아 배열에 저장
        players = FindObjectsOfType<PlayerController>();
        //Main Camera 찾기
        camera = Camera.main;
    }

    private void Update()
    {
        
    }

    //카메라 이동 제한 범위 그리기
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
        //카메라 이동의 최대 범위를 계산
        float maxX = boundary.width / 2f;
        float minX = -boundary.width / 2f;
        float maxY = boundary.height / 2f;
        float minY = -boundary.height / 2f;

        Vector3 targetPos = Vector3.zero;
        //Clamp를 통해 카메라 이동 범위를 제한
        targetPos = middlePoint;
        targetPos.x = Mathf.Clamp(middlePoint.x, minX, maxX);
        targetPos.y = Mathf.Clamp(middlePoint.y, minY, maxY);
        targetPos = Vector2.Lerp(targetPos, transform.position, 0f);
        //카메라의 z 갑을 -10으로 설정(설정하지 않으면 0이 되어 카메라에 오브젝트가 보이지 않음)
        targetPos.z = -10f;
        transform.position = targetPos;
    }
}
