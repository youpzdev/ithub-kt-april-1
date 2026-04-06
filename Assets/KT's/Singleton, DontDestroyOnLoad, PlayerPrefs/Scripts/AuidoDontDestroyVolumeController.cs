using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AuidoDontDestroyVolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private const string SaveKey = "MasterVolume";
    private const string MixerParameter = "Volume";

    private void Start()
    {
        float saved = PlayerPrefs.GetFloat(SaveKey, 1f);
        volumeSlider.value = saved;
        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
        Apply(saved);
    }

    private void OnSliderChanged(float value)
    {
        Apply(value);
        PlayerPrefs.SetFloat(SaveKey, value);
    } 

    private void Apply(float amount)
    {
        float db = Mathf.Log10(Mathf.Max(amount, 0.0001f)) * 20f;
        audioMixer.SetFloat(MixerParameter, db);
    }
}
