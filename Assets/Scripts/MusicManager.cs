using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    //mixer
    public AudioSource musicSource;
    private void Start()
    {
        musicSource.Play();
    }
}
