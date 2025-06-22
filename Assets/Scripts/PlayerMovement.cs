using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.8f;

    private float currentSpeed;
    private bool isGrounded;

    private Vector3 velocity;
    private CharacterController controller;

    public Transform cameraTransform;

    public  GameObject gameOverPanel;
    public  GameObject winPanel;
    public bool IsGameOver;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1;
        //IsGameOver == false;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        currentSpeed = (Input.GetKey(KeyCode.LeftShift)) ? sprintSpeed : moveSpeed;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        move.y = 0f;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }
        controller.Move(move.normalized * currentSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider collider)
    {
        // Перевіряємо, чи інший об'єкт має тег "Area"
        if (collider.gameObject.CompareTag("DeathArea"))
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            //IsGameOver == true;
            Debug.Log("GameOver");
            // Тут можна додати будь-яку логіку при зіткненні
        }
        else if (collider.gameObject.CompareTag("Chest"))
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
            //IsGameOver == true;
            Debug.Log("You win");
            // Тут можна додати будь-яку логіку при зіткненні
        }
    }
}



