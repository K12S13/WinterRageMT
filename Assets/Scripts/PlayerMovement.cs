using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Звичайна швидкість руху
    public float sprintSpeed = 10f;      // Швидкість при спринті
    public float jumpHeight = 2f;       // Висота стрибка
    public float gravity = -9.8f;       // Гравітація

    private float currentSpeed;         // Поточна швидкість руху
    private bool isGrounded;            // Чи на землі гравець

    private Vector3 velocity;           // Для зберігання руху по вертикалі (стрибок + гравітація)
    private CharacterController controller;

    public Transform cameraTransform;   // Камера персонажа

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Перевірка, чи на землі гравець
        isGrounded = controller.isGrounded;

        // Вибір швидкості руху (нормальний рух чи спринт)
        currentSpeed = (Input.GetKey(KeyCode.LeftShift)) ? sprintSpeed : moveSpeed;

        // Отримуємо ввід з клавіатури (WASD)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Рух відносно напрямку камери
        Vector3 move = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        move.y = 0f; // не даємо рухатись вверх-вниз

        // Стрибок (якщо персонаж на землі)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);  // Вираховуємо силу стрибка
        }

        // Якщо персонаж не на землі, застосовуємо гравітацію
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            // Якщо на землі, скидаємо вертикальну швидкість
            if (velocity.y < 0)
            {
                velocity.y = -2f;  // Легкий негатив для швидкого "прилипання" до землі
            }
        }

        // Рух по горизонталі
        controller.Move(move.normalized * currentSpeed * Time.deltaTime);

        // Рух по вертикалі (стрибок і гравітація)
        controller.Move(velocity * Time.deltaTime);
    }
}



