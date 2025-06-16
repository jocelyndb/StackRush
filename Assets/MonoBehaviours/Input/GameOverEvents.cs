using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverEvents : MonoBehaviour
{
    private UIDocument document;
    private Label rank;
    private Button startButton;
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

        if (PlayerPrefs.HasKey("LastRank"))
        {
            rank = document.rootVisualElement.Q("Rank") as Label;
            rank.text = PlayerPrefs.GetString("Rank");
        }

        startButton = document.rootVisualElement.Q("Start") as Button;
        startButton.RegisterCallback<ClickEvent>(OnStartClick);

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
        startButton.UnregisterCallback<ClickEvent>(OnStartClick);
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

    private void OnStartClick(ClickEvent e)
    {
        SceneManager.LoadScene("GameScene");
    }
}
