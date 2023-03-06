using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button back;
    [SerializeField] private Button restart;
    [SerializeField] private Button settings;
    [SerializeField] private Button exit;

    private void Awake()
    {
        pauseButton.onClick.AddListener(OpenPauseMenu);
        back.onClick.AddListener(ClosePauseMenu);

        restart.onClick.AddListener(Restart);
        settings.onClick.AddListener(Settings);
        exit.onClick.AddListener(Exit);
    }

    private void ClosePauseMenu()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
    }

    private void Restart()
    {
        Time.timeScale = 1f;

        sceneTransition.SwitchToScene("Game");
    }

    private void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    private void Exit()
    {
        Time.timeScale = 1f;

        sceneTransition.SwitchToScene("Menu");
    }

    private void OpenPauseMenu()
    {
        Time.timeScale = 0f;

        pauseMenu.SetActive(true);
    }
}
