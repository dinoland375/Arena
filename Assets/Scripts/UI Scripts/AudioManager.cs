using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider musicSlider = null;
    public Slider soundSlider = null;
    public AudioSource musicSource = null;
    public AudioSource[] soundSources = null;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", musicSlider.value);
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", soundSlider.value);

        musicSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChanged(); });
        soundSlider.onValueChanged.AddListener(delegate { OnSoundVolumeChanged(); });

        musicSource.volume = musicSlider.value;
        foreach (AudioSource source in soundSources)
        {
            source.volume = soundSlider.value;
        }
    }

    private void OnMusicVolumeChanged()
    {
        musicSource.volume = musicSlider.value;

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    private void OnSoundVolumeChanged()
    {
        foreach (AudioSource source in soundSources)
        {
            source.volume = soundSlider.value;
        }

        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
    }
}
