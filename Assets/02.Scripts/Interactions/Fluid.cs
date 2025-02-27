using Entity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Fluid : ObjectIdentifier
{
    [SerializeField] private ParticleSystem splashEffect;
    public AudioClip inWaterSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Ducky"))
            {
                SoundManager.PlayClip(inWaterSound);
            }
            
            splashEffect.transform.position = collision.transform.position; // 충돌 위치로 이동
            splashEffect.Play(); // 물 튀기는 이펙트 실행
        }

        if (LayerCheck(collision.gameObject.layer))
        {
            SignalManager.Instance.EmitSignal(SignalKey.GameOver);
        }
    }
}