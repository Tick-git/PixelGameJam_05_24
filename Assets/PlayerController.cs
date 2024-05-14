using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerStats _playerStats;
    
    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        _rigidbody.MovePosition(_rigidbody.position + movement * _playerStats.Speed * Time.fixedDeltaTime);
    }
}
