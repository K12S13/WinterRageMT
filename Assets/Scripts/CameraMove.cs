using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;
    public float clampAngle = 80f;
    
    private float rotX = 0f;
    private float rotY = 0f;
    public PlayerMovement UI;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // ховаємо і блокуюємо курсор
        Cursor.visible = false;                     // ховаємо курсор (додано)
    }
    
    void Update()
    {
        //if (UI.IsGameOver == false)
        //{ 
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

         // Ротація по осі X (горизонтально)
        transform.parent.Rotate(Vector3.up * mouseX);

            // Обмежуємо ротацію по осі Y (вертикально)
        rotY -= mouseY;
        rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);

        // Обертання камери по осі Y
        transform.localRotation = Quaternion.Euler(rotY, 0, 0);
        //}
    }
      
}


    


