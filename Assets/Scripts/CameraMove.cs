using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float sensitivityX = 2f;
    public float sensitivityY = 2f;
    public float clampAngle = 80f;

    private float rotY = 0f;

    private PlayerMovement playerMovement;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();

        LockCursor();
    }

    void Update()
    {
        bool isGameEnded =
            playerMovement.gameOverPanel.activeSelf ||
            playerMovement.winPanel.activeSelf;

        if (isGameEnded)
        {
            UnlockCursor();
            return;
        }

        LockCursor();

        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        transform.parent.Rotate(Vector3.up * mouseX);

        rotY -= mouseY;
        rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(rotY, 0, 0);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}