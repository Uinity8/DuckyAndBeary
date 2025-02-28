using UnityEngine;

namespace Entity.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class Platformer : MonoBehaviour
    {
        [SerializeField] Vector2 upDirection = Vector2.up;

        [Header("Floor Settings")] 
        [SerializeField] float floorMaxAngle = 45f;
        [SerializeField] float safeMargin = 0.08f;
        [SerializeField] private LayerMask floorLayer;
        
        [Header("Debug Options")] [SerializeField]
        bool debugRaysEnabled;
        
        Collider2D _collider;

        void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        /// <summary>
        /// 헬퍼 함수: Debug.DrawRay를 충돌 여부에 따라 색을 동적으로 그리도록 처리
        /// </summary>
        void DrawDebugRay(Vector2 origin, Vector2 direction, RaycastHit2D hit, Color defaultColor, Color hitColor)
        {
            if (debugRaysEnabled)
            {
                Color colorToUse = hit.collider != null ? hitColor : defaultColor;
                Debug.DrawRay(origin, direction, colorToUse);
            }
        }

        /// <summary>
        /// 노말 벡터의 각도가 floorMaxAngle 이내인지 확인
        /// </summary>
        bool IsFloorWithinMaxAngle(Vector2 normal)
        {
            float angle = Vector2.Angle(normal, upDirection);
            return angle <= floorMaxAngle;
        }

        /// <summary>
        /// 땅에 닿아 있는지 확인 (floorMaxAngle 조건 반영)
        /// </summary>
        public bool IsOnFloor()
        {
            Vector2 bottomCenter = new Vector2(_collider.bounds.center.x, _collider.bounds.min.y);
            RaycastHit2D hit = Physics2D.Raycast(bottomCenter, Vector2.down, safeMargin,floorLayer);

            // 디버그 레이 그리기 (충돌 시 빨간색, 비충돌 시 회색)
            DrawDebugRay(bottomCenter, Vector2.down * safeMargin, hit, Color.gray, Color.red);

            return hit.collider != null && IsFloorWithinMaxAngle(hit.normal);
        }

        /// <summary>
        /// 천장 감지
        /// </summary>
        public bool IsOnCeiling()
        {
            Vector2 topCenter = new Vector2(_collider.bounds.center.x, _collider.bounds.max.y);
            RaycastHit2D hit = Physics2D.Raycast(topCenter, Vector2.up, safeMargin, floorLayer);

            DrawDebugRay(topCenter, Vector2.up * safeMargin, hit, Color.gray, Color.cyan);

            return hit.collider != null;
        }

        /// <summary>
        /// 벽에 닿아 있는지 확인
        /// </summary>
        public bool IsOnWall()
        {
            Vector2 leftCenter = new Vector2(_collider.bounds.min.x, _collider.bounds.center.y);
            Vector2 rightCenter = new Vector2(_collider.bounds.max.x, _collider.bounds.center.y);

            RaycastHit2D hitLeft = Physics2D.Raycast(leftCenter, Vector2.left, safeMargin);
            RaycastHit2D hitRight = Physics2D.Raycast(rightCenter, Vector2.right, safeMargin);

            DrawDebugRay(leftCenter, Vector2.left * safeMargin, hitLeft, Color.gray, Color.green);
            DrawDebugRay(rightCenter, Vector2.right * safeMargin, hitRight, Color.gray, Color.green);

            return hitLeft.collider != null || hitRight.collider != null;
        }
    }
}