using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    void Start()
    {
        if (musicSlider != null)
        {
            float _musicVolume = PlayerPrefs.GetFloat("SavedMusicVolume", 1f);
            musicSlider.value = _musicVolume;
        }

        if (sfxSlider != null)
        {
            float _sfxVolume = PlayerPrefs.GetFloat("SavedSfxVolume", 1f);
            sfxSlider.value = _sfxVolume;
        }
    }


    void Update()
    {
        if (musicSlider != null)
        {
            AudioManager.instance.SetMusicVolume(musicSlider.value);
        }
        if (sfxSlider != null)
        {
            AudioManager.instance.SetSfxVolume(sfxSlider.value);
        }
    }
}
