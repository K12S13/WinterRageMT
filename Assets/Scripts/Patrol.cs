using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float waitTime = 1f;
    public float rotationSpeed = 5f;

    private int currentPointIndex = 0;
    private bool isWaiting = false;

    void Update()
    {
        if (patrolPoints.Length == 0)
            return;

        if (!isWaiting)
        {
            Transform targetPoint = patrolPoints[currentPointIndex];

            // Напрямок до точки
            Vector3 direction = (targetPoint.position - transform.position).normalized;

            // Прибираємо нахил вверх/вниз
            direction.y = 0;

            // Поворот до точки
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    lookRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            // Рух до точки
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPoint.position,
                speed * Time.deltaTime
            );

            // Досягли точки
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                isWaiting = true;
                Invoke(nameof(NextPoint), waitTime);
            }
        }
    }

    void NextPoint()
    {
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        isWaiting = false;
    }
}