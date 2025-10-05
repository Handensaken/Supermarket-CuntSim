using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Audio;
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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioResource peeSound, footstepsSound;

    [Serializable]
    struct ActionReferences
    {
        public InputActionReference move, look, attack, interact, pee;
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
        actionReferences.pee.action.performed += Allow;
        actionReferences.pee.action.canceled += Allow;
    }

    private void OnDisable()
    {
        actionReferences.move.action.performed -= Allow;
        actionReferences.move.action.canceled -= Allow;
        actionReferences.look.action.performed -= Allow;
        actionReferences.look.action.canceled -= Allow;
        actionReferences.attack.action.performed -= Allow;
        actionReferences.attack.action.canceled -= Allow;
        actionReferences.interact.action.started -= Interact;
        actionReferences.pee.action.performed -= Allow;
        actionReferences.pee.action.canceled -= Allow;
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

    private void Allow(InputAction.CallbackContext context)
    {
        if(context.action == actionReferences.move.action)
        {
            if (context.performed)
            {
                moving = true;
                animator.SetBool("Running", true);
                audioSource.resource = footstepsSound;
                audioSource.Play();
            }
            else if (context.canceled)
            {
                moving = false;
                animator.SetBool("Running", false);
                audioSource.Stop();
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
        
        else if(context.action == actionReferences.pee.action)
        {
            if (context.performed)
            {
                animator.SetBool("Pissing", true);
                audioSource.resource = peeSound;
                audioSource.Play();
            }
            else if (context.canceled)
            {
                animator.SetBool("Pissing", false);
                audioSource.Stop();
            }
        }
    }

    private void Move()
    {
        Vector2 direction = actionReferences.move.action.ReadValue<Vector2>();
        var scaledMoveSpeed = moveSpeed * Time.deltaTime;
        var moveVector = Quaternion.Euler(0, transform.eulerAngles.y + 270, 0) * new Vector3(direction.x, 0, direction.y);
        transform.position += moveVector * scaledMoveSpeed;
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

    private void Interact(InputAction.CallbackContext context)
    {
        animator.SetLayerWeight(2,1); 
        animator.SetTrigger("Interact");
        foreach (var mischief in currentMischiefEvents)
        {
            if (mischief.allowMischief)
            {
                mischief.OnMischief.Invoke();
            }
        }
    }

    public void SetLayerWeightToZero(int layer)
    {
        animator.SetLayerWeight(layer, 0);
    }
}
