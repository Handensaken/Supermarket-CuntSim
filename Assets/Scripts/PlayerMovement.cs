using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    private Vector2 rot;
    private bool moving, looking;

    [Serializable]
    struct ActionReferences
    {
        public InputActionReference move, look;
    }
    
    [SerializeField] private ActionReferences actionReferences;
    
    private PlayerInput playerInput;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        actionReferences.move.action.performed +=  AllowMove;
        actionReferences.move.action.canceled += AllowMove;
        actionReferences.look.action.performed += AllowLook;
        actionReferences.look.action.canceled += AllowLook;
    }

    private void OnDisable()
    {
        actionReferences.move.action.performed -= AllowMove;
        actionReferences.move.action.canceled -= AllowMove;
        actionReferences.look.action.performed -= AllowLook;
        actionReferences.look.action.canceled -= AllowLook;
    }

    public void Update()
    {
        if (moving)
        {
            Move();
        }

        if (looking)
        {
            Look();
        }
    }

    private void AllowMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moving = true;
        }
        else if (context.canceled)
        {
            moving = false;
        }
    }
    
    private void AllowLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            looking = true;
        }
        else if (context.canceled)
        {
            looking = false;
        }
    }

    private void Move()
    {
        Vector2 direction = actionReferences.move.action.ReadValue<Vector2>();
        if (direction.sqrMagnitude < 0.01) return;
        var scaledMoveSpeed = moveSpeed * Time.deltaTime;
        var moveVector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        transform.position += moveVector * scaledMoveSpeed;
    }

    private void Look()
    {
        var mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Ground")))
        {
            Vector3 targetPos = hit.point;
            Vector3 direction = targetPos - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            }
        }
    }
}
