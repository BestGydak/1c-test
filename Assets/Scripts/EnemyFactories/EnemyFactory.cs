using UnityEngine;

public abstract class EnemyFactory : ScriptableObject
{
    public abstract BaseEnemy Create();
}
