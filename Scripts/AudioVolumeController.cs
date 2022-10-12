using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioVolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI VolumeText;

    public float GetVolumeValue()
    {
        return volumeSlider.value;
    }
    private void Start()
    {
        UserData userData = UserData.GetData();
        if (userData != null)
        {
            volumeSlider.value = userData.volume;
            audio.volume = userData.volume;
            VolumeText.text = "ÇÂÓÊ: " + (volumeSlider.value * 100).ToString("0") + "%";
        }
    }
    public void VolumeUpdate()
    {
        audio.volume = volumeSlider.value;
        VolumeText.text = "ÇÂÓÊ: " + (volumeSlider.value * 100).ToString("0");
    }
}
