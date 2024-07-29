using System.Linq;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    [SerializeField] private BaseProjectile _baseProjectile;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _attackRadius;

    private float _currentCooldown;

    private void Update()
    {
        TryAttack();
    }

    private bool TryAttack()
    {
        _currentCooldown = Mathf.Max(0, _currentCooldown - Time.deltaTime);
        if (_currentCooldown > 0)
            return false;

        var possibleTargets =
            Physics2D.OverlapCircleAll(transform.position, _attackRadius)
            .Where(collider => collider.TryGetComponent<IDamageable>(out var _));
        if (!possibleTargets.Any())
            return false;

        var nearestTarget = possibleTargets.GetMinItem(collider => GetDistance(collider.transform));
        LaunchProjectile(nearestTarget.transform.position);
        _currentCooldown = _attackCooldown;
        return true;
    }

    private float GetDistance(Transform aTransform)
    {
        return Vector2.Distance(transform.position, aTransform.position);
    }

    private void LaunchProjectile(Vector2 targetsPosition)
    {
        var newProjectile = Instantiate(_baseProjectile, transform.position, Quaternion.identity);
        var launchDirection = (targetsPosition - (Vector2)transform.position).normalized;
        newProjectile.Launch(launchDirection);
    }
}

