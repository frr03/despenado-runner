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

    private void Start()
    {
        currentLifes = maxLifes;
        AtualizarLifeText(currentLifes);
    }

    public void TakeDamage(int qtd)
    {
        currentLifes -= qtd;
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
}
