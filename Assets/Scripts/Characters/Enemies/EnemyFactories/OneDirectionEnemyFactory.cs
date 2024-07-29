using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Factories/Standard", fileName = "One Direction")]
public class OneDirectionEnemyFactory : EnemyFactory
{
    [SerializeField] private OneDirectionEnemy _enemyPrefab;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Vector2 _moveDirection;

    public override BaseEnemy Create()
    {
        var enemy = Instantiate(_enemyPrefab);
        enemy.Health.SetHealth(_health);
        enemy.CurrentSpeed = Random.Range(_minSpeed, _maxSpeed);
        enemy.CurrentMoveDirection = _moveDirection;

        return enemy;
    }
}
