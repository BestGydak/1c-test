using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    public UnityEvent<BaseEnemy> Died;
    public abstract void Damage(int damage);
    public abstract void Kill();
}
