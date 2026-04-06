using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class SavedDataController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider volumeSlider;

    private string filePath;
    private float lastSavedVolume = -1f;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "SoundData.txt");
    }

    private void Start()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SoundData data = JsonConvert.DeserializeObject<SoundData>(json);
            SetVolume(data.Volume);
            Debug.Log($"Loaded volume: {data.Volume}");
        }
        else SaveVolume(audioSource.volume);

        volumeSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void Update() { if (!Mathf.Approximately(audioSource.volume, lastSavedVolume)) SaveVolume(audioSource.volume); }

    private void OnSliderChanged(float value) => audioSource.volume = value;

    private void SetVolume(float volume)
    {
        audioSource.volume = volume;
        volumeSlider.value = volume;
        lastSavedVolume = volume;
    }

    private void SaveVolume(float volume)
    {
        var data = new SoundData { Volume = volume };
        File.WriteAllText(filePath, JsonConvert.SerializeObject(data, Formatting.Indented));
        lastSavedVolume = volume;
    }
}

[Serializable]
public class SoundData
{
    public float Volume;
}
