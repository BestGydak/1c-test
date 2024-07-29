using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [field: SerializeField] public int CurrentHealth { get; private set; }

    public UnityEvent<Health> Died;
    public UnityEvent<Health, int, int> CurrentHealthChanged;

    public void SetHealth(int value)
    {
        if (value == CurrentHealth)
            return;

        var prevValue = CurrentHealth;
        CurrentHealth = value;

        CurrentHealthChanged.Invoke(this, prevValue, value);
        if(value <= 0)
        {
            Died.Invoke(this);
        }
    }

    public void Damage(int damage)
    {
        SetHealth(CurrentHealth - damage);
    }
}
