using UnityEngine;
using UnityEngine.UI;

public class DeadButton : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private Button restart;
    [SerializeField] private Button exit;

    void Start()
    {
        restart.onClick.AddListener(Restart);
        exit.onClick.AddListener(ExitToMenu);
    }

    private void Restart()
    {
        sceneTransition.SwitchToScene("Game");
    }

    private void ExitToMenu()
    {
        sceneTransition.SwitchToScene("Menu");
    }
}
