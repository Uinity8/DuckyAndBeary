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
}
