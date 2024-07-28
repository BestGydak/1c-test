using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private InputReader _inputReader;
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
        _rigidbody.velocity = _desiredMoveDirection.normalized * _speed;
    }

    private void OnMove(Vector2 value)
    {
        _desiredMoveDirection = value;
    }
}
