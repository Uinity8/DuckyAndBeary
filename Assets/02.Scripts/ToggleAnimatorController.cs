using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimatorController : MonoBehaviour
{
    private static readonly int IsOn = Animator.StringToHash("IsOn");
    public Toggle toggle;
    public Animator animator;

    void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        animator.SetBool(IsOn, isOn);
    }
}