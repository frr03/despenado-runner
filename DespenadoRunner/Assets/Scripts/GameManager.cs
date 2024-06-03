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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);

        Time.timeScale = 1f;
    }

    public void FecharJogo()
    {
        Application.Quit();
        Debug.Log("fechou o jogo.");
    }

    public void MudarFase()
    {
        int Cena = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(Cena + 1);

        Time.timeScale = 1f;
    }
}
