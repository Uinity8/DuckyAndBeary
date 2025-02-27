using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private Color glowColor = Color.yellow;
    
    
    //컬러가 변경될 스프라이트가 개별 오브젝트 최상단에 위치해야함
    [SerializeField] Switch[] switchObj;
    [SerializeField] DoorController[] doorObj;
    

    // 인스펙터에서 값 변경 시 자동 적용
    void OnValidate()
    {
        foreach (Switch obj in switchObj)
        {
            SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
            sr.color = glowColor;
        }
        
        foreach (DoorController obj in doorObj)
        {
            SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
            sr.color = glowColor;
        }
    }

    private void Awake()
    {
        foreach (Switch currentSwitch in switchObj)
        {
            foreach (DoorController door in doorObj)
            {
                currentSwitch.OnActive += door.SetActive;
            }
        }

    }

    public virtual bool ActiveSwitch() { return false; }


}
