using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public Slider _slider;
    public AudioSource _audioSource;
    private float _backVol = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _backVol = PlayerPrefs.GetFloat("BackVol", 1f);
        _slider.value = _backVol;
        _audioSource.volume = _slider.value;
    }

    private void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        _audioSource.volume = _slider.value;

        _backVol = _slider.value;
        PlayerPrefs.SetFloat("BackVol", _backVol);
    }
}
