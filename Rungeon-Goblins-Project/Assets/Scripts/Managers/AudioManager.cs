using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Components")]
    public static AudioManager Instance;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private AudioClip[] bgClips;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgSource;

    [Header("UI Components")]
    [SerializeField] Slider sfxSlider;
    private float sfxSliderPercentage;
    [SerializeField] Slider bgSlider;
    private float bgSliderPercentage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        LoadPlayerPrefSound("SFXVolume");
        LoadPlayerPrefSound("BGVolume");
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 10);
        PlayerPrefs.SetFloat("SFXVolume", Mathf.Log10(volume) * 10);
    }

    public void SetBGVolume(float volume)
    {
        audioMixer.SetFloat("BGVolume", Mathf.Log10(volume) * 10);
        PlayerPrefs.SetFloat("BGVolume", Mathf.Log10(volume) * 10);
    }

    public void PlaySFX(int index)
    {
        AudioClip clip = sfxClips[index];
        sfxSource.clip = clip;
        sfxSource.PlayOneShot(clip);
    }

    public void StopSFX(int index)
    {
        sfxSource.Stop();
    }

    public void PlayBG(int index)
    {
        bgSource.Stop();
        bgSource.clip = bgClips[index];
        bgSource.loop = false;
        bgSource.Play();
    }

    public void PlayBGLoop(int index)
    {
        bgSource.Stop();
        bgSource.clip = bgClips[index];
        bgSource.loop = true;
        bgSource.Play();

    }

    public IEnumerator Fade(AudioSource audio, float duration)
    {
        float time = 0, startVolume = audio.volume;
        audio.volume = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            audio.volume = Mathf.Lerp(0, startVolume, time / duration);
            yield return null;
        }

        yield break;
    }

    public void UpdateSoundInfo()
    {
        sfxSlider = GameObject.Find("SFX").GetComponent<Slider>();
        bgSlider = GameObject.Find("Music").GetComponent<Slider>();

        SyncSliderValueWithMixer("SFXVolume", sfxSlider);
        SyncSliderValueWithMixer("BGVolume", bgSlider);

        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        bgSlider.onValueChanged.AddListener(SetBGVolume);
    }

    private void SyncSliderValueWithMixer(string expose, Slider slider)
    {
        float value = PlayerPrefs.GetFloat(expose);
        float sliderValue = Mathf.Pow(10, value / 10);
        slider.value = sliderValue;
    }

    private void LoadPlayerPrefSound(string expose)
    {
        float value = PlayerPrefs.GetFloat(expose);
        audioMixer.SetFloat(expose, value);
    }
}
