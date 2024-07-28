using UnityEngine;
using UnityEngine.Events;

public class EnemyFinishLine : MonoBehaviour
{
    public UnityEvent<BaseEnemy> EnemyCrossed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out BaseEnemy enemy))
            return;

        EnemyCrossed.Invoke(enemy);
        enemy.Kill();
    }
}
