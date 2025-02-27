using Entity.Player;
using UnityEngine;

public class DoorController : Platformer
{
    [Header("DoorInfo")] [SerializeField] Transform targetTransform;
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
        if (!IsOnFloor())
            transform.position =
                Vector2.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.fixedDeltaTime);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Beary") | other.gameObject.CompareTag("Ducky"))
        {
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Beary") | other.gameObject.CompareTag("Ducky"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}