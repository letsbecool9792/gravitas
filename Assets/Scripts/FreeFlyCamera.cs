using UnityEngine;

public class FreeFlyCamera : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;
    public SimulationManager simManager;
    
    private bool isFlying = false;

    void Start()
    {
        // Lock cursor on start
        isFlying = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (simManager == null || !simManager.simulationRunning)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isFlying = !isFlying;
            Cursor.lockState = isFlying ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isFlying;
        }

        if (!isFlying) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float up = 0f;
        if (Input.GetKey(KeyCode.Space)) up += 1;
        if (Input.GetKey(KeyCode.LeftShift)) up -= 1;

        transform.position += (transform.forward * v + transform.right * h + Vector3.up * up) * moveSpeed * Time.deltaTime;

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        transform.Rotate(Vector3.up, mouseX, Space.World);
        transform.Rotate(Vector3.left, mouseY);
    }
}
