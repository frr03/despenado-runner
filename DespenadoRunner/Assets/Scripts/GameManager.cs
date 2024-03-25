using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject TelaDerrota;
    public GameObject TelaVitoria;

    public void Derrota()
    {
        TelaDerrota.SetActive(true);
    }

    public void Vitoria()
    {
        TelaVitoria.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void FecharJogo()
    {
        Application.Quit();
        Debug.Log("fechou o jogo.");
    }
}
