using UnityEngine;

public class HeadBobSystem : MonoBehaviour
{
    public FPSController controller; // referensi ke FPSController

    [Range(0.001f, 0.01f)]
    public float Amount = 0.002f;

    [Range(1f, 30f)]
    public float Frequency = 10.0f;

    [Range(1f, 20f)]
    public float Smooth = 10.0f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;

        // Optional auto-assign
        if (controller == null)
            controller = FindFirstObjectByType<FPSController>();
    }

    void Update()
    {
        if (controller == null) return;

        Vector3 input = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        bool isMoving = input.magnitude > 0.1f && controller.characterController.isGrounded;

        if (isMoving)
            StartHeadBob();
        else
            StopHeadBob();
    }

    private void StartHeadBob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * Frequency) * Amount;
        pos.x += Mathf.Cos(Time.time * Frequency / 2f) * Amount;

        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + pos, Smooth * Time.deltaTime);
    }

    private void StopHeadBob()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Smooth * Time.deltaTime);
    }
}

