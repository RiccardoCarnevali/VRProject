using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Slider ambienceVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private AudioMixer audioMixer;

    private void Start() {
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("mouse_sensitivity", Settings.defaultMouseSensitivity);
        ambienceVolumeSlider.value = PlayerPrefs.GetFloat("ambience_volume", Settings.defaultAmbienceVolume);
        audioMixer.SetFloat("Ambience", Mathf.Log10(ambienceVolumeSlider.value) * 20);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("sfx_volume", Settings.defaultSFXVolume);
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolumeSlider.value) * 20);
    }

    public void OnSensitivityChanged(float value) {
        PlayerPrefs.SetFloat("mouse_sensitivity", value);
        Messenger<float>.Broadcast(MessageEvents.MOUSE_SENSITIVITY_CHANGED, value, MessengerMode.DONT_REQUIRE_LISTENER);
    }

    public void OnAmbienceVolumeChanged(float volume){
        audioMixer.SetFloat("Ambience", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("ambience_volume", volume);
    }

    public void OnSFXVolumeChanged(float volume) {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolumeSlider.value) * 20);
        PlayerPrefs.SetFloat("sfx_volume", volume);
    }
}
