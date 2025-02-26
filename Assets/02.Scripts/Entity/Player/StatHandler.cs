using UnityEngine;

namespace Entity
{
    public class StatHandler : MonoBehaviour
    {
        [Range(0f, 1000f)] [SerializeField] float speed = 3f;

        public float Speed
        {
            get => speed;
            set => speed = Mathf.Clamp(value, 0, 20);
        }
    
        [Range(0f, 100f)] [SerializeField] float jumpForce = 5f;

        public float JumpForce
        {
            get => jumpForce;
            set => jumpForce = Mathf.Clamp(value, 0, 20);
        }
    }
}