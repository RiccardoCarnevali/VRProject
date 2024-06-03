using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;

    void Start(){
        if (volumeSlider != null){
            float volume = PlayerPrefs.GetFloat("Master", 0.5f);
            volumeSlider.value = volume;
            SetVolume(volume);
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume){
            audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("Master", volume);
        }
}

