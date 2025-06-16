using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource fxSource;

    [Header("Clips")]
    public AudioClip music;
    public AudioClip boxHit;
    public AudioClip wallHit;
    public AudioClip death;
    public AudioClip move;
    public AudioClip powerup;
    public AudioClip hover;
    public AudioClip click;

    private float targetPitch = 1f;
    private float pitchLerpFactor = 50f;

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Audio").Count() > 1)
        {
            Destroy(gameObject);
        }

        mixer.SetFloat("masterVol", Mathf.Log10(AudioSliders.loadVolume("masterVol")) * 20);
        mixer.SetFloat("musicVol", Mathf.Log10(AudioSliders.loadVolume("musicVol")) * 20);
        mixer.SetFloat("fxVol", Mathf.Log10(AudioSliders.loadVolume("fxVol")) * 20);


        DontDestroyOnLoad(transform.gameObject);
        musicSource.clip = music;
        // musicSource.Play();
        musicSource.Pause();
    }

    private void Update()
    {
        musicSource.pitch = Mathf.Lerp(musicSource.pitch, targetPitch, pitchLerpFactor * Time.deltaTime);
        fxSource.pitch = Mathf.Lerp(fxSource.pitch, targetPitch, pitchLerpFactor * Time.deltaTime);
    }

    public void PlaySFX(AudioClip clip)
    {
        fxSource.PlayOneShot(clip);
    }

    public void SetSlowMo(bool on)
    {
        targetPitch = on ? 0.5f : 1f;
    }

    public void PauseMusic()
    {
        musicSource.Stop();
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
}
