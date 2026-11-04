using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGazeMovement : MonoBehaviour
{
    private Transform cameraTransform;
    private float speed = 1.0f;
    private float tiltThresholdAngle = 20.0f;
    private bool moveByHeadTilt = true;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        bool shouldMove = false;

        if (moveByHeadTilt && cameraTransform != null)
        {
            float angleX = cameraTransform.eulerAngles.x;

            if (angleX > 180f)
            {
                angleX -= 360f;
            }

            if (angleX > tiltThresholdAngle && angleX < 80.0f)
            {
                shouldMove = true;
            }
        }

        if (Pointer.current != null && Pointer.current.press.isPressed)
        {
            shouldMove = true;
        }

        if (Google.XR.Cardboard.Api.IsTriggerHeldPressed)
        {
            shouldMove = true;
        }

        if (shouldMove && cameraTransform != null)
        {
            Vector3 moveDirection = cameraTransform.forward;
            moveDirection.y = 0;
            moveDirection.Normalize();
            Vector3 newPosition = moveDirection * speed * Time.deltaTime;
            transform.Translate(newPosition, Space.World);
        }
    }
}
