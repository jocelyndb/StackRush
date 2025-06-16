using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuEvents : MonoBehaviour
{
    private UIDocument document;
    private Button resumeButton;
    private Button menuButton;
    private List<Button> menuButtons;
    private AudioManager audioManager;

    private void OnEnable()
    {
        document = GetComponent<UIDocument>();

        resumeButton = document.rootVisualElement.Q("Resume") as Button;
        resumeButton.RegisterCallback<ClickEvent>(OnResumeClick);

        menuButton = document.rootVisualElement.Q("Menu") as Button;
        menuButton.RegisterCallback<ClickEvent>(OnMenuClick);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        menuButtons = document.rootVisualElement.Query<Button>().ToList();

        foreach (Button button in menuButtons)
        {
            button.RegisterCallback<ClickEvent>(delegate { audioManager.PlaySFX(audioManager.click); });
            button.RegisterCallback<MouseEnterEvent>(delegate { Debug.Log("Working here"); audioManager.PlaySFX(audioManager.hover); });
        }
    }

    private void OnDisable()
    {
        resumeButton.UnregisterCallback<ClickEvent>(OnResumeClick);
        resumeButton.UnregisterCallback<ClickEvent>(OnMenuClick);

        foreach (Button button in menuButtons)
        {
            button.UnregisterCallback<ClickEvent>(delegate { audioManager.PlaySFX(audioManager.click); });
            button.UnregisterCallback<MouseEnterEvent>(delegate { Debug.Log("Working here");  audioManager.PlaySFX(audioManager.hover); });
        }

        Debug.Log("Deregistered buttons");
    }

    private void Update()
    {
    }

    private void OnResumeClick(ClickEvent e)
    {
        Debug.Log("Resume pressed");
        GameManager.TogglePause();
        gameObject.SetActive(false);
    }

    private void OnMenuClick(ClickEvent e)
    {
        GameManager.TogglePause(true);
        SceneManager.LoadScene("MainMenuScene");
    }
}
