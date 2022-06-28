using UnityEngine;

public class NpcSpeechSalsa : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpeech(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
        
        Debug.Log("Character is Talking ID: " + clip);
    }

    public void StopSpeech()
    {
        _audioSource.Stop();
    }
}
