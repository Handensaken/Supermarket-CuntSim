using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 rot;
    private bool moving, looking, attacking;
    private Animator animator;
    [SerializeField] private List<MischiefEvent> currentMischiefEvents = new List<MischiefEvent>();

    [Serializable]
    struct ActionReferences
    {
        public InputActionReference move, look, attack, interact;
    }
    
    [SerializeField] private ActionReferences actionReferences;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetBool("Running", false);
    }

    private void OnEnable()
    {
        actionReferences.move.action.performed +=  Allow;
        actionReferences.move.action.canceled += Allow;
        actionReferences.look.action.performed += Allow;
        actionReferences.look.action.canceled += Allow;
        actionReferences.attack.action.performed += Allow;
        actionReferences.attack.action.canceled += Allow;
        actionReferences.interact.action.started += Interact;
    }

    private void OnDisable()
    {
        actionReferences.move.action.performed -= Allow;
        actionReferences.move.action.canceled -= Allow;
        actionReferences.look.action.performed -= Allow;
        actionReferences.look.action.canceled -= Allow;
        actionReferences.attack.action.performed -= Allow;
        actionReferences.attack.action.canceled -= Allow;
        actionReferences.interact.action.performed -= Interact;
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

        if (attacking)
        {
            Attack();
        }
    }

    private void Allow(InputAction.CallbackContext context)
    {
        if(context.action == actionReferences.move.action)
        {
            if (context.performed)
            {
                moving = true;
                animator.SetBool("Running", true);
            }
            else if (context.canceled)
            {
                moving = false;
                animator.SetBool("Running", false);
            }
        }
        else if(context.action == actionReferences.look.action)
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
        else if(context.action == actionReferences.attack.action)
        {
            if (context.performed)
            {
                attacking = true;
                animator.SetLayerWeight(1, 1);
            }
            else if (context.canceled)
            {
                attacking = false;
                animator.SetLayerWeight(1, 0);
            }
        }
    }

    private void Move()
    {
        Vector2 input = actionReferences.move.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        Look();
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
                transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y + 90, 0f);
            }
        }
    }

    private void Attack()
    {
        
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("tried interacting");
        foreach (var mischief in currentMischiefEvents)
        {
            if (mischief.allowMischief)
            {
                mischief.OnMischief.Invoke();
            }
        }
    }
}
