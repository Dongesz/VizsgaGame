using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Referencia a Slider komponenshez
    public AudioSource audioSource; // Referencia az AudioSource-hoz

    void Start()
    {
        // Alapértelmezett érték beállítása
        float defaultVolume = 0.2f;
        volumeSlider.value = defaultVolume;
        if (audioSource != null)
        {
            audioSource.volume = defaultVolume;
        }

        // Feliratkozás az OnValueChanged eseményre
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // A hangerő beállítása a slider aktuális értékére
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
