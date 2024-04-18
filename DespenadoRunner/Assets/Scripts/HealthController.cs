using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthController : MonoBehaviour
{
    public int maxLifes;
    public int currentLifes;

    public TextMeshProUGUI lifeText;

    public GameManager gameManager;

    private bool cheat = false;

    private void Start()
    {
        currentLifes = maxLifes;
        AtualizarLifeText(currentLifes);
    }

    public void TakeDamage(int qtd)
    {
        if (cheat == true)
        {
            Update();
            return;
        }

        currentLifes -= qtd;
        AtualizarLifeText(currentLifes);
        if (currentLifes <= 0)
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().Play("Waterphone");
            gameManager.Derrota();
        }
    }

    public void Instakill()
    {
        currentLifes = 0;
        AtualizarLifeText(currentLifes);
        if (currentLifes <= 0)
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().Play("Waterphone");
            gameManager.Derrota();
        }
    }

    private void AtualizarLifeText(int vida)
    {
        lifeText.text = currentLifes.ToString();
    }

    public void GainHealth(int qtd)
    {
        currentLifes += qtd;
        AtualizarLifeText(currentLifes);
    }

    //cheat de vidas infinitas
    public void Update()
    {
        if (SwipeManager.tap5)
        {
            Debug.Log("Cheat de vidas infinitas abilitado.");
            lifeText.text = "inf";
            cheat = true;
        }
    }
}
