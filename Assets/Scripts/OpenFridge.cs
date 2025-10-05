using UnityEngine;

[CreateAssetMenu (menuName = "Mischief Events/ Open Fridge")]
public class OpenFridge : MischiefEvent
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
        interactableObject.GetComponent<Animator>().SetTrigger("Open");
        AudioSource audioSource = interactableObject.GetComponent<AudioSource>();
        if (audioSource is null) return;
        audioSource.mute = false;
    }
}