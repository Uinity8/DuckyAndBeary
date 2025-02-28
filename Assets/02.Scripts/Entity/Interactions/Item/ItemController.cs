using Manager;
using UnityEngine;

public class ItemController : ObjectIdentifier
{
    GameManager gameManager;
    public AudioClip getScore;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerCheck(collision.gameObject.layer))
        {
            SoundManager.PlayClip(getScore);
            Debug.Log("점수 획득");
            gameManager.AddScore(1);
            Destroy(this.gameObject);
        }
    }
}
