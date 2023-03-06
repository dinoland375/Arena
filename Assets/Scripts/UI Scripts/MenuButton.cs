using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button backButton;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderEfx;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettingsMenu);
        backButton.onClick.AddListener(CloseSettingsMenu);
        exitButton.onClick.AddListener(ExitTheGame);
    }

    private void StartGame()
    {
        sceneTransition.SwitchToScene("Game");
    }

    private void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    private void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    private void ExitTheGame()
    {
        Application.Quit();
    }
}
