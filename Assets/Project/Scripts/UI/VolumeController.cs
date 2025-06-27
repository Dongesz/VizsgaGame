// @desc: Controls global audio volume using a UI slider linked to an AudioSource
// @lastWritten: 2025-06-27
// @upToDate: false
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

        // Ha a valtozo erteke megvaltozik, meghivja a setvolume-ot
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
