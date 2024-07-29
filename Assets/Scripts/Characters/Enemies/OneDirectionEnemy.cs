using UnityEngine;

public class OneDirectionEnemy : BaseEnemy
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] protected Animator _animator;
    [SerializeField] private float _currentSpeed;
    [field: SerializeField] public Vector2 CurrentMoveDirection { get; set; } 

    [Header("Sound Settings")]
    [SerializeField] private SFX _hurtSFX;
    [SerializeField] private SoundManagerEventChannel _soundChannel;
    [field: SerializeField] public Health Health { get; private set; }


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
        _animator.SetBool("IsRunning", true);
        _animator.SetFloat("MovementX", CurrentMoveDirection.x);
        _animator.SetFloat("MovementY", CurrentMoveDirection.y);
        _rigidbody.velocity = _currentSpeed * CurrentMoveDirection.normalized;
    }

    private void OnEnable()
    {
        Health.CurrentHealthChanged.AddListener(OnCurrentHealthChanged);
        Health.Died.AddListener(OnDied);
    }

    private void OnDisable()
    {
        Health.CurrentHealthChanged.RemoveListener(OnCurrentHealthChanged);
        Health.Died.RemoveListener(OnDied);
    }

    public override void Kill(DamageType damageType)
    {
        Health.SetHealth(0, damageType);
    }

    private void OnDied(Health _, DamageType damageType)
    {
        Died.Invoke(this, damageType);
        Destroy(gameObject);
    }

    private void OnCurrentHealthChanged(Health _, int prevHp, int newHp)
    {
        if (newHp >= prevHp)
            return;
        _soundChannel.RequestPlay(_hurtSFX);
    }
}
