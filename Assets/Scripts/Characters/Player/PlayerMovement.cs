using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;

    private Vector2 _desiredMoveDirection = Vector2.zero;

    private void OnEnable()
    {
        _inputReader.Moved += OnMove;
    }

    private void OnDisable()
    {
        _inputReader.Moved -= OnMove;
    }

    private void FixedUpdate()
    {
        var normalizedDirection = _desiredMoveDirection.normalized;
        _rigidbody.velocity = normalizedDirection * _speed;
        Animate(normalizedDirection);
    }

    private void Animate(Vector2 direction)
    {
        if(_animator == null)
            return;
        if (direction == Vector2.zero)
            _animator.SetBool("IsRunning", false);
        else
        {
            _animator.SetBool("IsRunning", true);
            _animator.SetFloat("MovementX", direction.x);
            _animator.SetFloat("MovementY", direction.y);
        }
    }

    private void OnMove(Vector2 value)
    {
        _desiredMoveDirection = value;
    }
}
