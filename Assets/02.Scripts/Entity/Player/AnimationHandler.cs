using UnityEngine;

namespace Entity
{
    public class AnimationHandler : MonoBehaviour
    {
        static readonly int SpeedHash = Animator.StringToHash("Speed");
        static readonly int VelocityY = Animator.StringToHash("VelocityY");
        static readonly int Grounded = Animator.StringToHash("IsGrounded");
        static readonly int JumpHash = Animator.StringToHash("Jump");

        Animator animator;

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void Move(float speed)
        {
            animator.SetFloat(SpeedHash, Mathf.Abs(speed));
        }

        public void Jump()
        {
            animator.SetTrigger(JumpHash);
        }
        
        public void FallOrLand(float velocityY)
        {
            animator.SetFloat(VelocityY, velocityY);
        }
        
        public void IsGrounded(bool isGrounded)
        {
            animator.SetBool(Grounded, isGrounded);
        }
        
    }
}
