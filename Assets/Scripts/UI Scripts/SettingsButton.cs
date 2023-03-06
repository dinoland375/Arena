using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject changeButtonText;
    [SerializeField] private GameObject joystick;
    [SerializeField] private Button back;
    [SerializeField] private Button changeButton;
    [SerializeField] private Button saveChange;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button ultaAttackButton;
    [SerializeField] private Button attackButton;

    public bool changeButtonActive;

    private void Awake()
    {
        back.onClick.AddListener(BackInPauseMenu);
        changeButton.onClick.AddListener(ChangeButtonPosition);
        saveChange.onClick.AddListener(SaveChangeButton);
    }

    private void Update()
    {
        if (changeButtonActive)
        {
            joystick.GetComponent<FixedJoystick>().enabled = false;
            pauseButton.GetComponent<Button>().enabled = false;
            ultaAttackButton.GetComponent<Button>().enabled = false;
            attackButton.GetComponent<Button>().enabled = false;
        }
        else 
        { 
            joystick.GetComponent<FixedJoystick>().enabled = true;
            pauseButton.GetComponent<Button>().enabled = true;
            ultaAttackButton.GetComponent<Button>().enabled = true;
            attackButton.GetComponent<Button>().enabled = true;
        }
    }

    private void BackInPauseMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    private void ChangeButtonPosition()
    {
        changeButtonActive = true;
        settingsMenu.SetActive(false);
        changeButtonText.SetActive(true);
    }

    private void SaveChangeButton()
    {
        settingsMenu.SetActive(true);
        changeButtonText.SetActive(false);
        changeButtonActive = false;
    }
}
