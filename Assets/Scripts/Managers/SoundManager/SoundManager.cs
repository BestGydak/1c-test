using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SoundManagerEventChannel _listenedChannel;

    private void OnEnable()
    {
        _listenedChannel.PlayRequested += OnPlayRequest;
    }

    private void OnDisable()
    {
        _listenedChannel.PlayRequested -= OnPlayRequest;
    }

    private void OnPlayRequest(SFX sfx)
    {
        _audioSource.PlayOneShot(sfx.Clip, sfx.VolumeScale);
    }
}
