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
        if (!interactableObject.GetComponent<Animator>()) return;
        Animator animator = interactableObject.GetComponent<Animator>();
        interactableObject.GetComponent<Animator>().SetTrigger("Open");
        if (!interactableObject.GetComponent<AudioSource>()) return;
        AudioSource audioSource = interactableObject.GetComponent<AudioSource>();
        audioSource.Play();
        allowMischief = false;
    }
}