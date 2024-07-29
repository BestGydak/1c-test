using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Type", menuName = "Damage Type")]
public class DamageType : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
}
