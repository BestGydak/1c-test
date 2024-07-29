using UnityEngine;
using UnityEngine.Events;

public class EnemyFinishLine : MonoBehaviour
{
    [SerializeField] private DamageType _gameDamage;
    [SerializeField] private SFX _enemyKillSFX;
    [SerializeField] private SoundManagerEventChannel _soundChannel;
    public UnityEvent<BaseEnemy> EnemyCrossed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out BaseEnemy enemy))
            return;

        EnemyCrossed.Invoke(enemy);
        enemy.Kill(_gameDamage);
        _soundChannel.RequestPlay(_enemyKillSFX);
    }
}
