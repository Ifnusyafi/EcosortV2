using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 20f;

    [Header("Camera Settings")]
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    [Header("Player State")]
    public bool canMove = true;

    public CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        #region Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        Vector3 move = (forward * curSpeedX) + (right * curSpeedY);
        #endregion

        #region Jump & Gravity
        if (characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space) && canMove)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = 0f; // Jangan pakai -1f agar tidak memaksa player turun terus
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        #endregion

        // Gabungkan pergerakan horizontal dan vertikal
        Vector3 finalMove = move;
        finalMove.y = moveDirection.y;

        #region Apply Movement
        characterController.Move(finalMove * Time.deltaTime);
        #endregion

        #region Camera & Rotation
        if (canMove)
        {
            // Vertical look
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // Horizontal look
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion

        #region Cursor Unlock (for testing)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        #endregion
    }
}
