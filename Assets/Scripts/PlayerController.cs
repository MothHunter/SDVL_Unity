using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 4f;

    public Animator animator;
    private Rigidbody2D rb;

    private Vector2 moveInput;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        rb.velocity = moveInput * moveSpeed;
        if (moveInput.magnitude != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        AnimateMovement(moveInput);        
    }

    public void SetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void AnimateMovement(Vector3 direction)
    {
        if(animator != null)
        {
            animator.SetBool("isMoving", isMoving);
            if(isMoving)
            {
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
        }
    }
}
