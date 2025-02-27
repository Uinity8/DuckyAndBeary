using Entity.Player;
using Unity.VisualScripting;
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
        SignalManager signalManager;

        float moveInputX;

        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] bool isDuckCharacter;
        private bool isPaused;

        public bool IsDuckCharacter => isDuckCharacter;


        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            platformer = GetComponent<Platformer>();
            animationHandler = GetComponent<AnimationHandler>();
            statHandler = GetComponent<StatHandler>();
            signalManager = SignalManager.Instance;
        }

        private void Start()
        {
            signalManager.ConnectSignal(SignalKey.GamePaused, OnPaused);
            signalManager.ConnectSignal(SignalKey.GameResumed, OnResume);
        }

        void Update()
        {
            HandleAnimationUpdates();
        }

        void FixedUpdate()
        {
            MovePlayer();
        }

        void OnPaused(object sender)
        {
            Debug.Log("게임이 멈췄습니다");
            isPaused = true;
            Debug.Log("신호를 받았습니다.");

        }
        public void OnResume(object sender)
        {
            isPaused = false;
        }
        void OnMove(InputValue value)
        {

            if (isPaused) return;
            moveInputX = value.Get<float>();
            FlipSprite(moveInputX);
            animationHandler.Move(moveInputX);
        }

        void OnJump(InputValue value)
        {
            if (isPaused) return;
            if (!platformer.IsOnFloor()) return;


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