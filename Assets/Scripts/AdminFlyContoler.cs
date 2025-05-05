using UnityEngine;

public class AdminFlyContoler : MonoBehaviour
{
    public bool canFly = false; // მოწმდება ჩართულია თუ არა ფრენა
    public float flySpeed = 10f; // ფრენის სიჩქარე
    public float verticalSpeed = 5f;
    private Vector3 moveDirection;

    void Update()
    {
        // ფრენის დროს მოძრაობა
        if (canFly)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");
            float moveY = 0f;

            if (Input.GetKey(KeyCode.Space)) moveY = 1f; // მაღლა ფრენა
            if (Input.GetKey(KeyCode.LeftControl)) moveY = -1f; // ქვემოთ ფრენა

            Camera cam = Camera.main;
            Vector3 camForward = cam.transform.forward;
            Vector3 camRight = cam.transform.right;

            moveDirection = (camForward * moveZ + camRight * moveX).normalized * flySpeed;
            moveDirection.y = moveY * verticalSpeed;

            transform.Translate(moveDirection * Time.deltaTime, Space.World);
        }
    }
    // ფრენის ვოიდები, ასევე Debug შემოწმება
    public void ActivateFlyMode()
    {
        canFly = true;
        Debug.Log("ფრენა ჩართულია");
    }

    public void DeactivateFlyMode()
    {
        canFly = false;
        Debug.Log("ფრანა გამრთულია");
    }
}
