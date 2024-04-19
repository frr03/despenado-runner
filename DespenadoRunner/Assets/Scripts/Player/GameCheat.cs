using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameCheat : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject PauseButton;

    void Update()
    {
        if (SwipeManager.tap4)
        {
            Time.timeScale = 0f;
            PauseButton.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Vitoria");
            gameManager.Vitoria();
        }
    }
}