using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Manager Event Channel", menuName = "Sound Manager/Event Channel")]
public class SoundManagerEventChannel : ScriptableObject
{
    public event Action<SFX> PlayRequested;

    public void RequestPlay(SFX sfx)
    {
        PlayRequested?.Invoke(sfx);
    }
}
