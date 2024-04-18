using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameCheat : MonoBehaviour
{
    public GameManager gameManager;

    void Update()
    {
        if (SwipeManager.tap4)
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().Play("Vitoria");
            gameManager.Vitoria();
        }
    }
}