// @desc: Loads all SFX AudioClips dynamically from Resources, manages SFX and Music volume sliders, and plays sounds via key-based or direct reference.
// @lastWritten: 2025-07-08
// @upToDate: true
// @TODO: Remove PlaySfx(AudioClip sfx), Cause: Redundant method 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using System;
using System.IO;
namespace CastL.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public Slider SfxVolumeSlider;
        public Slider MusicVolumeSlider;
        public AudioSource SfxAudioSource;
        public AudioSource MusicAudioSource;

        public Dictionary<string, AudioClip> clips;

        private void Awake()
        {
            Instance = this;
           
            clips = new Dictionary<string, AudioClip>();

            AudioClip[] loadedClips = Resources.LoadAll<AudioClip>("SFX");

            foreach(var clip in loadedClips)
            {
                clips.Add(clip.name, clip);
                Debug.Log($"{clip.name} - {clip}");
            }
        }

        private void Start()
        {
            // SFX slider setup
            float sfxDefaultVolume = 0.5f;
            SfxVolumeSlider.value = sfxDefaultVolume;
            if (SfxAudioSource != null)
            {
                SfxAudioSource.volume = sfxDefaultVolume;
            }
            SfxVolumeSlider.onValueChanged.AddListener(SetVolumeSfx);

            // Music slider setup
            float musicDefaultVolume = 0.5f;
            MusicVolumeSlider.value = musicDefaultVolume;
            if (MusicAudioSource != null)
            {
                MusicAudioSource.volume = musicDefaultVolume;
            }
            MusicVolumeSlider.onValueChanged.AddListener(SetVolumeMusic);
        }

        public void PlaySfx(AudioClip sfx)
        {
            SfxAudioSource.PlayOneShot(sfx);
        }
        public void PlaySfx(string key)
        {
            if (clips.TryGetValue(key, out var clip))
            {
                SfxAudioSource.PlayOneShot(clip);
            }
        }
        public void SetVolumeSfx(float volume)
        {
            if (SfxAudioSource != null)
            {
                SfxAudioSource.volume = volume;
            }
        }

        public void SetVolumeMusic(float volume)
        {
            if (MusicAudioSource != null)
            {
                MusicAudioSource.volume = volume;
            }
        }
    }
}
