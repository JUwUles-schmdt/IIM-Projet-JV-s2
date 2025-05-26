using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float chaseDistance = 5f;
    public Transform player;

    private Vector3 currentTarget;
    private SpriteRenderer spriteRenderer;
    private bool returningToZone = false;
    private float fixedY; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentTarget = pointB.position;
        fixedY = transform.position.y; 
    }

    void Update()
    {
        if (!IsInsideLimits(transform.position))
        {
            returningToZone = true;
            ReturnToZone();
            return;
        }

        if (returningToZone)
        {
            returningToZone = false;
            currentTarget = ClosestPatrolPoint().position;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool playerInRange = distanceToPlayer < chaseDistance;
        bool playerInsideLimits = IsInsideLimits(player.position);

        if (playerInRange && playerInsideLimits)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Vector3 targetPos = new Vector3(currentTarget.x, fixedY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        spriteRenderer.flipX = (targetPos.x < transform.position.x);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            currentTarget = currentTarget == pointA.position ? pointB.position : pointA.position;
        }
    }

    void ChasePlayer()
    {
        Vector3 targetPos = new Vector3(player.position.x, fixedY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        spriteRenderer.flipX = (player.position.x < transform.position.x);
    }

    void ReturnToZone()
    {
        Transform closestPoint = ClosestPatrolPoint();
        Vector3 targetPos = new Vector3(closestPoint.position.x, fixedY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        spriteRenderer.flipX = (targetPos.x < transform.position.x);
    }

    bool IsInsideLimits(Vector3 pos)
    {
        float minX = Mathf.Min(pointA.position.x, pointB.position.x);
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x);
        return pos.x >= minX && pos.x <= maxX;
    }

    Transform ClosestPatrolPoint()
    {
        float distA = Vector3.Distance(transform.position, pointA.position);
        float distB = Vector3.Distance(transform.position, pointB.position);
        return distA < distB ? pointA : pointB;
    }
}
