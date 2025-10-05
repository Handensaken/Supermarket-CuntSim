using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu (menuName = "Mischief Events/ Fire Clerk")]
public class FireClerk : MischiefEvent
{
    private void OnEnable()
    {
        OnMischief.AddListener(Mischief);
    }

    private void OnDisable()
    {
        OnMischief.RemoveListener(Mischief);
    }
    
    private void Mischief()
    {
        allowMischief = false;
        if (!interactableObject.GetComponent<Animator>()) return;
        Animator animator = interactableObject.GetComponent<Animator>();
        interactableObject.GetComponent<Animator>().SetTrigger("Start");
        
        if (!interactableObject.GetComponent<MischiefHandler>()) return;
        MischiefHandler mischief = interactableObject.GetComponent<MischiefHandler>();
        mischief.enabled = false;
        
        VisualEffect ve = interactableObject.GetComponentInChildren<VisualEffect>();
        if (ve is null) return;
        ve.enabled = true;
    }
}
