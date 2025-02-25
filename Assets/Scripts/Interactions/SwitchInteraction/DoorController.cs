using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("DoorInfo")] 
    [SerializeField] Transform targetTransform;
    [SerializeField] private float moveSpeed;

    private Vector2 intialPosition;
    private Vector2 desiredPosition;
    
    private void Start()
    {
        intialPosition = transform.position;
        desiredPosition = transform.position;
    }

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.fixedDeltaTime);
    }


    public void SetActive(bool isActive)
    {
        Debug.Log("Door Active");

        if (isActive)
        {
            desiredPosition = targetTransform.position;
        }
        else
        {
            desiredPosition = intialPosition;
        }
    }
}