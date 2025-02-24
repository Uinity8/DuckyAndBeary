using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 direction;
    public float speed = 3;
    public float jumpPower = 10;
    bool isJump = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horzontal = Input.GetAxisRaw("Horizontal");
        direction = new Vector2(horzontal, 0).normalized;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        direction = direction * speed;

        rigid.velocity =  direction;

        if (isJump)
        {
            Jump();
        }
    }   

    void Jump()
    {
        Vector2 velocity = rigid.velocity;
        velocity.y += jumpPower;

        isJump = false;

        rigid.velocity = velocity;
    }
}
