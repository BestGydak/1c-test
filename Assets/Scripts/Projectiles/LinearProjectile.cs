using UnityEngine;

public class LinearProjectile : BaseProjectile
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _liveTimeInSeconds;

    private bool _hasDamaged;

    private void Start()
    {
        Destroy(gameObject, _liveTimeInSeconds);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject;

        if(collisionObject.TryGetComponent<IDamageable>(out var damageable) &&
            !_hasDamaged)
        {
            damageable.Damage(_damage);
            Destroy(gameObject);
            return;
            _hasDamaged = true;
        }

        Destroy(gameObject);
    }

    public override void Launch(Vector2 direction)
    {
        _rigidbody.velocity = _speed * direction;
    }
}
