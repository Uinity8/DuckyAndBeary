using Entity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Fluid : ObjectIdentifier
{
    [SerializeField] private ParticleSystem splashEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            splashEffect.transform.position = collision.transform.position; // 충돌 위치로 이동
            splashEffect.Play(); // 물 튀기는 이펙트 실행
        }
        
        if (LayerCheck(collision.gameObject.layer))
        {
            StatHandler bear = collision.GetComponent<StatHandler>();

            if (bear != null)
            {
                Debug.Log($"{collision.gameObject.name}이 물에 닿았습니다.");
                
                GameManager.Instance.GameOver();
                //bear.Death() //플레이어 컴포넌트 안에 있는 사망 메서드 호출
                //바다에 빠진 파티클 이펙트 재생
            }
        }
    }
}
