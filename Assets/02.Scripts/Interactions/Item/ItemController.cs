using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : ObjectIdentifier
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerCheck(collision.gameObject.layer))
        {
            Debug.Log("점수 획득");
            gameManager.AddScore(1);
            Destroy(this.gameObject);
        }
    }
}
