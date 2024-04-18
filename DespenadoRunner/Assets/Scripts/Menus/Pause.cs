using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class Pause : MonoBehaviour
{
    [SerializeField] private HealthController _hc;
    public static bool JogoPausado = false;
    public GameObject MenuDePause;
    public GameObject PauseButton;

    public void Continuar()
    {
        PauseButton.SetActive(true);
        MenuDePause.SetActive(false);
        Time.timeScale = 1f;
        JogoPausado = false;
    }

    public void Pausar()
    {
        PauseButton.SetActive(false);
        MenuDePause.SetActive(true);
        Time.timeScale = 0f;
        JogoPausado = true;
    }
}