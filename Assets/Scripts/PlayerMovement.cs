using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 rot;
    private bool moving, looking;

    [Serializable]
    struct ActionReferences
    {
        public InputActionReference move, look, attack;
    }
    
    [SerializeField] private ActionReferences actionReferences;

    private void OnEnable()
    {
        actionReferences.move.action.performed +=  AllowMove;
        actionReferences.move.action.canceled += AllowMove;
        actionReferences.look.action.performed += AllowLook;
        actionReferences.look.action.canceled += AllowLook;
        actionReferences.attack.action.performed += ctx => Debug.Log("Attack!");
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
        Vector2 input = actionReferences.move.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void Look()
    {
        var mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
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
