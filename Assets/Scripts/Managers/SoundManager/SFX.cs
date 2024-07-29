using UnityEngine;

[CreateAssetMenu (fileName = "SFX", menuName = "Sound Manager/SFX")]
public class SFX : ScriptableObject
{
    [field: SerializeField] public AudioClip Clip { get; private set; }
    [field: SerializeField, Range(0,1)] public float VolumeScale { get; private set; }
}
