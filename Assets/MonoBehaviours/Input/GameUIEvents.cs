using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIEvents : MonoBehaviour
{
    private UIDocument document;
    private Button pauseButton;
    private ProgressBar progressBar;
    private Label scoreText;
    private Label powerups;
    private GameObject pauseMenu;
    private int score;
    private AudioManager audioManager;
    private List<Button> menuButtons;


    // private float progressBarLerpFactor = 20f;
    public float progressBarLerpFactor = 100f;
    private float progressBarVelocity = 0f;
    public float progressBarDamping = 0.99f;
    // private float scoreLerpFactor = 10f;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        pauseButton = document.rootVisualElement.Q("PauseButton") as Button;
        pauseButton.RegisterCallback<ClickEvent>(OnPauseClick);
        pauseButton.RegisterCallback<MouseEnterEvent>(OnPauseClick);

        progressBar = document.rootVisualElement.Q("BlocksStacked") as ProgressBar;
        progressBar.value = GameManager.Instance.LevelCount - 1f;

        scoreText = document.rootVisualElement.Q("Score") as Label;
        score = GameManager.Instance.score;

        powerups = document.rootVisualElement.Q("Powerups") as Label;

        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);

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
        pauseButton.UnregisterCallback<ClickEvent>(OnPauseClick);
        pauseButton.UnregisterCallback<MouseEnterEvent>(OnPauseClick);

        foreach (Button button in menuButtons)
        {
            button.UnregisterCallback<ClickEvent>(delegate { audioManager.PlaySFX(audioManager.click); });
            button.UnregisterCallback<MouseEnterEvent>(delegate { Debug.Log("Working here");  audioManager.PlaySFX(audioManager.hover); });
        }
    }

    private void Update()
    {
        progressBar.title = GameManager.GetLevelName();

        // Naive increase score
        score += score < GameManager.Instance.score ? 1 : 0;
        scoreText.text = $"Score: {score}";

        // Display powerups
        string listedPowerups = "";
        foreach (Powerup powerup in GameManager.Instance.powerups)
        {
            listedPowerups += powerup.name + "\n";
        }
        powerups.text = listedPowerups;
    }

    private void FixedUpdate()
    {
        progressBar.value = NumberAcceleration(progressBar.value, GameManager.Instance.LevelCount - 1f, Time.fixedDeltaTime * progressBarLerpFactor, ref progressBarVelocity);
    }

    private void OnPauseClick(MouseEnterEvent evt)
    {
        Debug.Log("Mouse hover");
    }

    private void OnPauseClick(ClickEvent e)
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        GameManager.TogglePause();
        Debug.Log("Pause pressed");
    }

    private float NumberAcceleration(float a, float b, float t, ref float v)
    {
        v *= progressBarDamping;
        v += (b - a) * t;
        return a + v * t;
    }
}
