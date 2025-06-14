using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class PauseMenuEvents : MonoBehaviour
{
    private UIDocument document;
    private Button resumeButton;
    private Button menuButton;
    private Slider volSlider;
    private Slider musicSlider;
    private Slider fxSlider;
    private AudioMixer mixer;

    private void OnEnable()
    {
        document = GetComponent<UIDocument>();

        resumeButton = document.rootVisualElement.Q("Resume") as Button;
        resumeButton.RegisterCallback<ClickEvent>(OnResumeClick);

        mixer = GameObject.FindFirstObjectByType<AudioMixer>();
        Debug.Log( mixer);
        Debug.Log(document);

        menuButton = document.rootVisualElement.Q("Menu") as Button;
        menuButton.RegisterCallback<ClickEvent>(OnMenuClick);

        // TODO: deal with volume setting!
        volSlider = document.rootVisualElement.Q("VolSlider") as Slider;
        musicSlider = document.rootVisualElement.Q("MusicSlider") as Slider;
        fxSlider = document.rootVisualElement.Q("FXSlider") as Slider;
        Debug.Log("Registered buttons");


    }

    private void OnDisable()
    {
        resumeButton.UnregisterCallback<ClickEvent>(OnResumeClick);
        resumeButton.UnregisterCallback<ClickEvent>(OnMenuClick);
        Debug.Log("Deregistered buttons");
    }

    private void Update()
    {
    }

    private void OnResumeClick(ClickEvent e)
    {
        // Time.timeScale = 1;
        GameManager.TogglePause();
        Debug.Log("Resume pressed");
        gameObject.SetActive(false);
    }

    private void OnMenuClick(ClickEvent e)
    {
        // TODO: make set menu scene
        // Time.timeScale = 1;
        GameManager.TogglePause();
        Debug.Log("Menu pressed");
        gameObject.SetActive(false);
    }
}
