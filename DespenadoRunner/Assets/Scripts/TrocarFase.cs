using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarFase : MonoBehaviour
{
    public int Fase;

    public void SelecionarFase()
    {
        SceneManager.LoadScene(Fase);
        Time.timeScale = 1f;
    }
}