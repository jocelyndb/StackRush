using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
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
        score = GameManager.Instance.Score;

        powerups = document.rootVisualElement.Q("Powerups") as Label;

        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    private void OnDisable()
    {
        pauseButton.UnregisterCallback<ClickEvent>(OnPauseClick);
        pauseButton.UnregisterCallback<MouseEnterEvent>(OnPauseClick);
    }

    private void Update()
    {
        // Smooth increase progress bar
        // progressBar.title = $"Level  {GameManager.Instance.Level}";
        progressBar.title = GameManager.GetLevelName();
        // progressBar.value = Mathf.Lerp(progressBar.value, GameManager.Instance.LevelCount - 1f, Time.deltaTime * progressBarLerpFactor);

        // Naive increase score
        score += score < GameManager.Instance.Score ? 1 : 0;
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
        progressBar.value = numberAcceleration(progressBar.value, GameManager.Instance.LevelCount - 1f, Time.fixedDeltaTime * progressBarLerpFactor, ref progressBarVelocity);
    }

    private void OnPauseClick(MouseEnterEvent evt)
    {
        Debug.Log("Mouse hover");
    }

    private void OnPauseClick(ClickEvent e)
    {
        pauseMenu.SetActive(true);
        GameManager.TogglePause();
        // Time.timeScale = 0;

        Debug.Log("Pause pressed");
    }

    private float numberAcceleration(float a, float b, float t, ref float v)
    {
        v *= progressBarDamping;
        v += (b - a) * t;
        return a + v * t;
    }
}
