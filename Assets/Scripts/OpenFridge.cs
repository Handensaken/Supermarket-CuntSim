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
        Debug.Log("Opening Fridge");
    }
}
