using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEnemy : MonoBehaviour
{
    public UnityEvent<BaseEnemy, DamageType> Died;
    public abstract void Kill(DamageType damageType);
}