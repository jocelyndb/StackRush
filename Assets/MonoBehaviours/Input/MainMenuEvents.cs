using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    public Scene gameScene;
    public Scene optionsScene;

    private UIDocument document;
    private Button startButton;
    private Button optionsButton;
    private List<Button> menuButtons = new List<Button>();
    // private Slider volSlider;
    // private Slider musicSlider;
    // private Slider fxSlider;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        document = GetComponent<UIDocument>();

        startButton = document.rootVisualElement.Q("Start") as Button;
        startButton.RegisterCallback<ClickEvent>(OnStartClick);

        optionsButton = document.rootVisualElement.Q("Options") as Button;
        optionsButton.RegisterCallback<ClickEvent>(OnOptionsClick);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();

        foreach (Button button in menuButtons)
        {
            button.RegisterCallback<ClickEvent>(delegate { audioManager.PlaySFX(audioManager.click); });
            button.RegisterCallback<MouseEnterEvent>(delegate { audioManager.PlaySFX(audioManager.hover); });
        }
    }

    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(OnStartClick);
        optionsButton.UnregisterCallback<ClickEvent>(OnOptionsClick);

        foreach (Button button in menuButtons)
        {
            button.UnregisterCallback<ClickEvent>(delegate { audioManager.PlaySFX(audioManager.click); });
            button.UnregisterCallback<MouseEnterEvent>(delegate { Debug.Log("Working here");  audioManager.PlaySFX(audioManager.hover); });
        }
        
        Debug.Log("Deregistered buttons");
    }

    private void OnOptionsClick(ClickEvent e)
    {
        SceneManager.LoadScene("OptionsScene");
    }

    private void OnStartClick(ClickEvent e)
    {
        SceneManager.LoadScene("GameScene");
    }
}
