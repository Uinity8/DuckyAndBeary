using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : ObjectIdentifier
{
    public bool isOpen;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerCheck(collision.gameObject.layer))
        {
            DoorOpen();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (LayerCheck(collision.gameObject.layer))
        {
            DoorClose();
        }
    }
    private void DoorOpen()
    {
        isOpen = true;
        Debug.Log("문 열림");
        gameManager.GameClearCheck();
        //animator.SetBool(IsOpen, true);
        //gamemanager.Instace.IsClear();
    }
    private void DoorClose()
    {
        isOpen = false;
        Debug.Log("문 닫힘");
        //animator.SetBool(IsOpen,false);
        //gamemanager.Instace.IsClear();
    }
}
