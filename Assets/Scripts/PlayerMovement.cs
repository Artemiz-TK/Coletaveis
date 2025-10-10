using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerPhysic;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 3f;
    private bool isOnGround;
    private bool canJump;
    private bool jumpQueued;

    private Vector3 input;

    void OnEnable()
    {
        if (moveAction.action != null)
        {
            moveAction.action.performed += OnMove;
            moveAction.action.canceled += ctx => input = Vector3.zero;
            moveAction.action.Enable();
        }

        if (jumpAction.action != null)
        {
            jumpAction.action.performed += OnJump;
            jumpAction.action.Enable();
        }
    }

    void OnDisable()
    {
        if (moveAction.action != null)
        {
            moveAction.action.performed -= OnMove;
            moveAction.action.canceled -= ctx => input = Vector3.zero;
            moveAction.action.Disable();
            moveAction.action.Dispose();
        }

        if (jumpAction.action != null)
        {
            jumpAction.action.performed -= OnJump;
            jumpAction.action.Disable();
            jumpAction.action.Dispose();
        }
    }

    void FixedUpdate()
    {
        if (isOnGround && canJump && jumpQueued)
        {
            playerPhysic.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpQueued = false;
        }
        
        Vector3 move = (transform.forward * input.z + transform.right * input.x) * speed;
        Vector3 newVelocity = new Vector3(move.x, playerPhysic.linearVelocity.y, move.z);
        playerPhysic.linearVelocity = newVelocity;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input2D = context.ReadValue<Vector2>();
        input = new Vector3(input2D.x, 0f, input2D.y);
    }

    void OnJump(InputAction.CallbackContext context)
    {
        canJump = false;
        isOnGround = false;
        jumpQueued = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canJump = true;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canJump = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
            canJump = false;
        }
    }
}
