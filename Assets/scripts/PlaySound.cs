using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] AudioClip soundClip;
    [SerializeField, Range(0f, 1f)] float volume = 1f;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D: same volume wherever the listener is
    }

    // OnEnable runs every time the panel is shown; Start only runs the first time
    void OnEnable()
    {
        if (soundClip == null)
        {
            Debug.LogWarning("No soundClip assigned in the Inspector.", this);
            return;
        }
        audioSource.PlayOneShot(soundClip, volume);
    }
}
