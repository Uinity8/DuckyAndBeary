using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("DoorInfo")]
    //(Red : 0 , Blue : 1)
    [SerializeField] private int doorColorID;

    //(Lever : 0 / Button : 1)
    [SerializeField] private int doorTypeID;


    [Header("DoorMoveInfo")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 prevPosition;
    [SerializeField] private Vector2 desiredPosition;
    
    private LeverController[] levers;
    private ButtonController[] buttons; 

    private void Awake()
    {
        levers = FindObjectsOfType<LeverController>();
        buttons = FindObjectsOfType<ButtonController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = prevPosition;
    }

    private void FixedUpdate()
    {
        CheckMatchSwitch();
    }

    void CheckMatchSwitch()
    {
        for (int i = 0; i < levers.Length; i++)
        {
            if (levers[i].ActiveSwitch() && 
                levers[i].SwitchTypeID == doorTypeID && 
                levers[i].SwitchColorID == doorColorID)
            {
                DesiredPositionr();
            }
            else
            {
                PrevPosition();
            }
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].ActiveSwitch() &&
                buttons[i].SwitchTypeID == doorTypeID &&
                buttons[i].SwitchColorID == doorColorID)
            {
                DesiredPositionr();
            }
            else
            {
                PrevPosition();
            }
        }
    }

    //스위치 활성화 시 이동
    void DesiredPositionr()
    {
        //스위치 온 목표지점까지 이동
        transform.position = Vector2.Lerp(
            transform.position,
            desiredPosition,
            moveSpeed * Time.fixedDeltaTime);
    }

    //스위치 비활성화 시 이동
    void PrevPosition()
    {                
        //스위치 오프 원래지점까지 이동
        transform.position = Vector2.Lerp(
            transform.position,
            prevPosition,
            moveSpeed * Time.fixedDeltaTime);
    }
}
