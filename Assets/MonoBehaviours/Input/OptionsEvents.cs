using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionsEvents : MonoBehaviour
{
    private UIDocument document;
    private Label scores;
    private Button menuButton;
    private List<Button> menuButtons = new List<Button>();
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        document = GetComponent<UIDocument>();

        scores = document.rootVisualElement.Q("Scores") as Label;
        if (PlayerPrefs.HasKey("LastRank"))
        {
            string lastRank = PlayerPrefs.GetString("LastRank");
            string bestRank = PlayerPrefs.GetString("Rank");
            scores.text = $"Your best rank was {bestRank},\nyour last rank was {lastRank}";
        }
        else
        {
            scores.text = "";
        }

        menuButton = document.rootVisualElement.Q("MenuButton") as Button;
        menuButton.RegisterCallback<ClickEvent>(OnMenuClick);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();

        foreach (Button button in menuButtons)
        {
            button.RegisterCallback<ClickEvent>(delegate { audioManager.PlaySFX(audioManager.click); });
            button.RegisterCallback<MouseEnterEvent>(delegate { Debug.Log("Working here");  audioManager.PlaySFX(audioManager.hover); });
        }
    }

    private void OnDisable()
    {
        menuButton.UnregisterCallback<ClickEvent>(OnMenuClick);

        foreach (Button button in menuButtons)
        {
            button.UnregisterCallback<ClickEvent>(delegate { audioManager.PlaySFX(audioManager.click); });
            button.UnregisterCallback<MouseEnterEvent>(delegate { Debug.Log("Working here");  audioManager.PlaySFX(audioManager.hover); });
        }

        Debug.Log("Deregistered buttons");
    }

    private void OnMenuClick(ClickEvent e)
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
