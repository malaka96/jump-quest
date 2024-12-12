using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    //reference to audiomixer
    public AudioMixer audioMixer;

    [SerializeField] private AudioClip bananaSfx;
    [SerializeField] private AudioClip spikeSfx;
    [SerializeField] private AudioClip victorySfx;
    [SerializeField] private AudioClip allBananasSfx;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        LoadSavedVolume();

    }

    private void LoadSavedVolume()
    {

        float _musicVolume = PlayerPrefs.GetFloat("SavedMusicVolume", 1f);
        audioMixer.SetFloat("Music", Mathf.Log10(_musicVolume) * 20);

        float _sfxVolume = PlayerPrefs.GetFloat("SavedSfxVolume", 1f);
        audioMixer.SetFloat("SFX", Mathf.Log10(_sfxVolume) * 20);
    }

    public void PlayBananaSfx()
    {
        audioSource.PlayOneShot(bananaSfx);
    }

    public void PlaySpikeSfx()
    {
        audioSource.PlayOneShot(spikeSfx);
    }

    public void PlayVictorySfx()
    {
        audioSource.PlayOneShot(victorySfx);
    }

    public void PlayAllBananasSfx()
    {
        audioSource.PlayOneShot(allBananasSfx);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SavedMusicVolume", volume);
    }
    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SavedSfxVolume", volume);
    }
}
