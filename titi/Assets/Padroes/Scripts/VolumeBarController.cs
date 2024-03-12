using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeBarController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; 
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
    }

    private void OnVolumeSliderChanged(float volume)
    {
        // manda o video para o youtube
        AudioObserverManager.VolumeChanged(volume);
    }

  
}
