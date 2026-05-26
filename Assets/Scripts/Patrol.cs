using UnityEngine;

public class Patrol : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float waitTime = 1f;
    public float rotationSpeed = 5f;

    [Header("Chase")]
    public Transform player;
    public float chaseSpeed = 4f;
    public float stopDistance = 6f;

    [Header("Raycast")]
    public Transform rayPoint;
    public float viewDistance = 15f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootDelay = 2f;
    public float bulletSpeed = 20f;

    private int currentPointIndex = 0;
    private bool isWaiting = false;
    private bool isPatroling = true;

    private float shootTimer = 0f;

    void Update()
    {
        CheckPlayer();

        if (isPatroling)
        {
            patrol();
        }
        else
        {
            Chase();
        }
    }

    void CheckPlayer()
    {
        RaycastHit hit;

        Debug.DrawRay(rayPoint.position, rayPoint.forward * viewDistance, Color.red);

        if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, viewDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                player = hit.collider.transform;
                isPatroling = false;
            }
        }
    }

    void NextPoint()
    {
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        isWaiting = false;
    }

    void patrol()
    {
        if (patrolPoints.Length == 0)
            return;

        if (!isWaiting)
        {
            Transform targetPoint = patrolPoints[currentPointIndex];

            Vector3 direction = (targetPoint.position - transform.position).normalized;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    lookRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPoint.position,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                isWaiting = true;
                Invoke(nameof(NextPoint), waitTime);
            }
        }
    }

    void Chase()
    {

        float distance = Vector3.Distance(transform.position, player.position);

        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        if (distance > stopDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                chaseSpeed * Time.deltaTime
            );
        }

        if (distance <= stopDistance)
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= shootDelay)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        Vector3 direction = player.position - shootPoint.position;
        direction.y = 0f;
        direction = direction.normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            shootPoint.position,
            Quaternion.LookRotation(direction)
        );

        Bullet bulletState = bullet.GetComponent<Bullet>();
        bulletState.isPlayerBullet = false;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.linearVelocity = direction * bulletSpeed;

        Debug.Log("Enemy shoot");
    }
}