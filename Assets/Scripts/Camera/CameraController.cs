using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //PlayerController���� ã�� �迭�� ����
    PlayerController[] players;
    Camera camera;

    [Header("ī�޶� �̵� ���� ����")]
    [SerializeField] Rect boundary;
    [SerializeField] Color gizmoColor;

    [Header("ī�޶� �� ��&�ƿ� ����")]
    [SerializeField] float distanceWeight;
    [SerializeField] float minValue;
}
