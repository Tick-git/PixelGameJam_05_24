using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerStats _playerStats;
    
    Rigidbody2D _rigidbody;
    Animator _animator;
    
    Vector2 _movementVector;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        _animator.SetFloat("FacingDirection", _movementVector.x);
    }

    void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movementVector * _playerStats.Speed * Time.fixedDeltaTime);
    }
}
