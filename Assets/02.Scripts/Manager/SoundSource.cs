using UnityEngine;

public class SoundSource : MonoBehaviour
{
    private AudioSource audioSource;

    public void PlaySound(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        CancelInvoke();
        audioSource.clip = clip;
        audioSource.volume = soundEffectVolume;
        audioSource.Play();
        audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        Invoke(nameof(Disable), clip.length + 2);
    }

    public void Disable()
    {
        audioSource?.Stop();
        Destroy(this.gameObject);
    }
}
