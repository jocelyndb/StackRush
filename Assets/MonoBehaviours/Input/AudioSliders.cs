using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class AudioSliders : MonoBehaviour
{
    public AudioMixer mixer;
    private UIDocument document;
    private Slider masterSlider;
    private Slider musicSlider;
    private Slider fxSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        document = GetComponent<UIDocument>();

        masterSlider = document.rootVisualElement.Q("VolSlider") as Slider;
        musicSlider = document.rootVisualElement.Q("MusicSlider") as Slider;
        fxSlider = document.rootVisualElement.Q("FXSlider") as Slider;

        masterSlider.value = loadVolume("masterVol");
        musicSlider.value = loadVolume("musicVol");
        fxSlider.value = loadVolume("fxVol");

        masterSlider.RegisterCallback<ChangeEvent<float>>(delegate { setVolume(masterSlider.value, "masterVol"); });
        musicSlider.RegisterCallback<ChangeEvent<float>>(delegate { setVolume(musicSlider.value, "musicVol"); });
        fxSlider.RegisterCallback<ChangeEvent<float>>(delegate { setVolume(fxSlider.value, "fxVol"); });

        setVolume(masterSlider.value, "masterVol");
        setVolume(musicSlider.value, "musicVol");
        setVolume(fxSlider.value, "fxVol");
    }

    private void setVolume(float value, string name)
    {
        mixer.SetFloat(name, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(name, value);
    }

    public static float loadVolume(string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return PlayerPrefs.GetFloat(name);
        }
        return 1f;
    }
}
