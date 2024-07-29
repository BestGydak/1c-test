using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int CurrentHealth { get; private set; }

    public UnityEvent<Health, DamageType> Died;
    public UnityEvent<Health, int, int> CurrentHealthChanged;

    public void SetHealth(int value, DamageType damageType = null)
    {
        if (value == CurrentHealth)
            return;

        var prevValue = CurrentHealth;
        CurrentHealth = value;

        CurrentHealthChanged.Invoke(this, prevValue, value);
        if(value <= 0)
        {
            Died.Invoke(this, damageType);
        }
    }

    public void Damage(int damage, DamageType damageType)
    {
        SetHealth(CurrentHealth - damage, damageType);
    }
}
