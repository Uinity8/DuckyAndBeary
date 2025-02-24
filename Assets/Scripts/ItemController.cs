using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : ObjectIdentifier
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerCheck(collision.gameObject.layer))
        {
            Debug.Log("점수 획득");
            //gameManager.AddScore();
            Destroy(this.gameObject);
        }
    }
}
