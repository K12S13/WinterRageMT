using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;
    public float clampAngle = 80f;
    
    private float rotX = 0f;
    private float rotY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // ховаємо і блокуюємо курсор
        Cursor.visible = false;                     // ховаємо курсор (додано)
    }
    
    void Update()
    {
        // Отримуємо рух миші
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        // Ротація по осі X (горизонтально)
        transform.parent.Rotate(Vector3.up * mouseX);

        // Обмежуємо ротацію по осі Y (вертикально)
        rotY -= mouseY;
        rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);

        // Обертання камери по осі Y
        transform.localRotation = Quaternion.Euler(rotY, 0, 0);
    }
}


    
    // public Transform player;         // Посилання на гравця
    // public Vector3 offset;           // Відстань камери від гравця

    // public float sensitivity = 3f;   // Чутливість мишки
    // public float smoothSpeed = 0.125f; // Швидкість плавного слідування

    // public Transform cameraPivot;    // Поворот камери

    // private float rotationX = 0f;
    // private float rotationY = 0f;

    // void Start()
    // {
    //     Cursor.lockState = CursorLockMode.Locked;  // Локалізація курсора
    //     Cursor.visible = false;
    // }

    // void Update()
    // {
    //     // Читання вводу миші для обертання камери
    //     rotationX += Input.GetAxis("Mouse X") * sensitivity;
    //     rotationY -= Input.GetAxis("Mouse Y") * sensitivity;

    //     // Обмеження по вертикальному обертанню
    //     rotationY = Mathf.Clamp(rotationY, -50f, 80f);

    //     // Обертання камери (повільно за допомогою Slerp)
    //     Quaternion targetRotation = Quaternion.Euler(rotationY, rotationX, 0f);
    //     cameraPivot.localRotation = Quaternion.Slerp(cameraPivot.localRotation, targetRotation, smoothSpeed);

    //     // Плавне слідування камери (за допомогою Lerp)
    //     Vector3 targetPosition = player.position + offset;
    //     transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    // }

