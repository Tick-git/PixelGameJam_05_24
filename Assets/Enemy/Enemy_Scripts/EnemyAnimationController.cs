using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    Animator _animationController;

    Transform _target;

    private void Awake()
    {
        _animationController = GetComponent<Animator>();
    }

    private void Start()
    {
        _target = GetComponent<EnemyTargetBehavior>().Target;
    }

    void Update()
    {
        Vector2 targetDirection = _target.position - transform.position;

        _animationController.SetFloat("FacingDirection", targetDirection.x);
    }
}
