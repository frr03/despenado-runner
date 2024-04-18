using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PontosController : MonoBehaviour
{
    public int points;
    public int currentPoints;

    public TextMeshProUGUI pointText;

    //public GameManager gameManager;

    private void Start()
    {
        currentPoints = points;
        AtualizarPointsText(currentPoints);
    }

    public void IncreaseScore(int qtd)
    {
        currentPoints += qtd;
        AtualizarPointsText(currentPoints);
        /*if (currentLifes <= 0)
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().Play("Waterphone");
            gameManager.Derrota();
        }*/
    }

    private void AtualizarPointsText(int ponto)
    {
        pointText.text = currentPoints.ToString();
    }

    public void GainHealth(int qtd)
    {
        currentPoints += qtd;
        AtualizarPointsText(currentPoints);
    }
}
