using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float patrolSpeed = 5f;
    [SerializeField] float chaseSpeed = 5f;
    [SerializeField] float pointThreshold = 0.1f;

    [Header("Chase Settings")]
    [SerializeField] Transform player;
    [SerializeField] float chaseRange = 6f;
    [SerializeField] float escapeRange = 10f;
    
    [Header("Rotation Settings")]
    public float rotationSpeed = 720f;
    public bool spriteFacesUp;
    
    int _currentPointIndex;
    bool _isChasing;
    Rigidbody2D _rb;

    void Awake ()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        if (player == null)
            return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (_isChasing && distanceToPlayer >= escapeRange)
            _isChasing = false;
        else if (distanceToPlayer <= chaseRange)
            _isChasing = true;
        
        if (_isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
            return;
        
        Transform targetPoint = patrolPoints[_currentPointIndex];
        MoveAndRotate(targetPoint.position);
        
        if (Vector2.Distance(transform.position, targetPoint.position) < pointThreshold)
            _currentPointIndex = (_currentPointIndex + 1) % patrolPoints.Length;
    }

    void ChasePlayer()
    {
        MoveAndRotate(player.position);
    }
    
    void MoveAndRotate(Vector3 targetPosition)
    {
        Vector2 targetPos2D = new(targetPosition.x, targetPosition.y);
        
        float moveSpeed = _isChasing ? chaseSpeed : patrolSpeed;
        Vector2 nextPosition = Vector2.MoveTowards(_rb.position, targetPos2D, moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(nextPosition);
        
        Vector2 direction = targetPos2D - _rb.position;
        
        if (direction == Vector2.zero)
            return;
        
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (spriteFacesUp)
            targetAngle -= 90f;
        
        float smoothAngle = Mathf.MoveTowardsAngle(_rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        
        _rb.MoveRotation(smoothAngle);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}