using UnityEngine;
using UnityEngine.Events;

public abstract class BaseEnemy : MonoBehaviour
{
    public UnityEvent<BaseEnemy> Died;
    public abstract void Kill();
}
