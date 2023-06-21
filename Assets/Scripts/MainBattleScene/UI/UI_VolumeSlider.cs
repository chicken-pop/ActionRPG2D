using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public string parameter;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float multiplier;

    private void Awake()
    {
        //UI‚ª•\Ž¦‚³‚ê‚Ä‚¢‚È‚¯‚ê‚Î‚È‚ç‚È‚¢
        slider.onValueChanged.AddListener((value) => { SliderValue(value); });
    }

    public void SliderValue(float _value)
    {
        audioMixer.SetFloat(parameter, Mathf.Log10(_value) * multiplier);
    }

    public void LoadSlider(float _value)
    {
        //Awake‚Åˆê”ÔÅ‰‚ÉLoad‚³‚ê‚é
        if(_value > 0.001f)
        {
            slider.value = _value;
        }
    }
}
