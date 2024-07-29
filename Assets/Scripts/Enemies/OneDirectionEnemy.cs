using UnityEngine;

public class OneDirectionEnemy : BaseEnemy, IDamageable
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private int _currentHealth;
    [SerializeField] private float _currentSpeed;

    public Vector2 CurrentMoveDirection; 

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = Mathf.Max(value, 0);
            if (_currentHealth != 0)
                return;
        
            Died.Invoke(this);
            Destroy(gameObject);
        
        }
    }

    public float CurrentSpeed
    {
        get => _currentSpeed;
        set
        {
            if(value < 0)
            {
                Debug.LogError("Speed cannot be negative!");
            }
            _currentSpeed = value;
        }
    }

    private void Start()
    {
        _rigidbody.velocity = _currentSpeed * CurrentMoveDirection.normalized;
    }

    public override void Damage(int damage)
    {
        CurrentHealth -= damage;
    }

    public override void Kill()
    {
        CurrentHealth = 0;
    }
}
