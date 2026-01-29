using UnityEngine;

public class audioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Files")]
    public AudioClip[] musicFiles;
    public AudioClip[] sfxFiles;
    void Start()
    {
        musicSource.clip = musicFiles[0];
        musicSource.Play();
    }

    public void PlaySFX(int index)
    {
        sfxSource.PlayOneShot(sfxFiles[index-1]);
    }
}
