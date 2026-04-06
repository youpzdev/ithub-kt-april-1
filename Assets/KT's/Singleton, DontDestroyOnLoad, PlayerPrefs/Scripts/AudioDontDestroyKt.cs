using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer), typeof(AudioSource))]
public class AudioDontDestroyKt : MonoBehaviour
{
    public static AudioDontDestroyKt Instance {get; private set; }
    [SerializeField] private AudioMixerGroup audioMixer;
    [SerializeField] private VideoClip videoClip;

    private AudioSource audioSource;
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.outputAudioMixerGroup = audioMixer;

        videoPlayer.clip = videoClip;
        videoPlayer.isLooping = true;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
