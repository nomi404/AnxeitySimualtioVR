using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRPlayerMovement : MonoBehaviour
{
    public float speed = 3.0f; // Movement speed
    private CharacterController characterController;
    private XROrigin xrRig; // Reference to the XR Rig
    private Vector2 inputAxis;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        xrRig = GetComponent<XROrigin>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputAxis = context.ReadValue<Vector2>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Get the direction based on head orientation
        Transform headTransform = xrRig.Camera.transform;
        Vector3 direction = new Vector3(inputAxis.x, 0, inputAxis.y);
        direction = headTransform.TransformDirection(direction);
        direction.y = 0; // Prevent movement in the Y-axis

        // Move the player
        characterController.Move(direction * speed * Time.deltaTime);
    }
}