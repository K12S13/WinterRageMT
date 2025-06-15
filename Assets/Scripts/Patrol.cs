using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;  // Массив точок патрулювання
    public float speed = 2f;          // Швидкість руху
    public float waitTime = 1f;       // Час зупинки на точці патрулювання

    private int currentPointIndex = 0;  // Поточна точка патрулювання
    private bool isWaiting = false;     // Чи чекає об'єкт на точці?

    void Update()
    {
        if (patrolPoints.Length == 0)
            return;

        // Якщо об'єкт на точці патрулювання
        if (!isWaiting)
        {
            // Переміщаємось до поточної точки
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

            // Якщо ми досягли точки
            if (transform.position == patrolPoints[currentPointIndex].position)
            {
                isWaiting = true;
                Invoke("NextPoint", waitTime);  // Чекаємо перед переходом до наступної точки
            }
        }
    }

    // Функція для переходу до наступної точки патрулювання
    void NextPoint()
    {
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;  // Переміщаємось до наступної точки
        isWaiting = false;  // Відновлюємо рух
    }
}

