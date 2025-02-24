using Entity.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity
{
    public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    Platformer platformer;
    AnimationHandler animationHandler;
    StatHandler statHandler;

    float moveInputX;
    
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] bool isDuckCharacter;
    public bool IsDuckCharacter => isDuckCharacter;


    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        platformer = GetComponent<Platformer>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
    }
    
    void Update()
    {
        HandleAnimationUpdates();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void OnMove(InputValue value)
    {
        if(UIManager.Instance.IsPaused) return;
        
        moveInputX = value.Get<float>();
        FlipSprite(moveInputX);
        animationHandler.Move(moveInputX);
    }

    void OnJump(InputValue value)
    {
        if (!platformer.IsOnFloor()) return;
        if(UIManager.Instance.IsPaused) return;
        

        _rigidbody2D.AddForce(statHandler.JumpForce * Vector2.up, ForceMode2D.Impulse);
        animationHandler.Jump();
    }

    // 애니메이션 업데이트
    void HandleAnimationUpdates()
    {
        animationHandler.FallOrLand(_rigidbody2D.velocity.y);
        animationHandler.IsGrounded(platformer.IsOnFloor());
    }

    // 플레이어 이동
    void MovePlayer()
    {
        _rigidbody2D.velocity = new Vector2(moveInputX * statHandler.Speed, _rigidbody2D.velocity.y);
    }

    // 캐릭터 방향 전환
    void FlipSprite(float velocityX)
    {
        if (velocityX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (velocityX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
}