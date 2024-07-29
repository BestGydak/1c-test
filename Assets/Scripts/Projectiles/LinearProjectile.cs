using UnityEngine;

public class LinearProjectile : BaseProjectile
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _liveTimeInSeconds;
    [SerializeField] private DamageType _intendedDamage;

    public void Start()
    {
        Destroy(gameObject, _liveTimeInSeconds);    
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject;

        if(collisionObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(_damage, _intendedDamage);
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    public override void Launch(Vector2 direction)
    {
        _rigidbody.velocity = _speed * direction;
    }
}
