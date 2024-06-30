using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private DamageFeedback df;
    [SerializeField] private HealthController _hc;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MovimentoLateral ml;

    public GameObject PauseButton;

    // PowerUp Milho
    private bool speedBoost = false;
    private float count_M;

    // PowerUp Escudo
    private float count_e;
    private bool hasShield = false;
    public GameObject escudo;

    // Perder Velocidade ao tomar Dano
    private bool speedDown = false;
    private float count_D;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        UpdateMovementSpeed();
        UpdateShield();
        UpdateSpeedBoost();
        UpdateSpeedDown();
    }

    private void UpdateMovementSpeed()
    {
        if (speedBoost && !speedDown)
        {
            ml.VelocidadeBoost();
        }
        else if (speedDown && !speedBoost)
        {
            ml.VelocidadeDown();
        }
        else
        {
            ml.VelocidadeNormal();
        }
    }

    private void UpdateShield()
    {
        if (hasShield)
        {
            escudo.SetActive(true);
            count_e -= Time.deltaTime;
            if (count_e <= 0)
            {
                hasShield = false;
                escudo.SetActive(false);
            }
        }
    }

    private void UpdateSpeedBoost()
    {
        if (speedBoost)
        {
            count_M -= Time.deltaTime;
            if (count_M <= 0)
            {
                speedBoost = false;
            }
        }
    }

    private void UpdateSpeedDown()
    {
        if (speedDown)
        {
            count_D -= Time.deltaTime;
            if (count_D <= 0)
            {
                speedDown = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Milho":
                ActivateSpeedBoost();
                Destroy(other.gameObject);
                break;
            case "Egg":
                ActivateShield();
                Destroy(other.gameObject);
                break;
            case "Pontuador":
                Pontos.QtdPts += 2;
                Destroy(other.gameObject);
                break;
            case "Limite":
                HandleVictory();
                break;
            case "Item":
                HandleItemPickup();
                Destroy(other.gameObject);
                break;
            case "Obstaculo":
                HandleObstacleCollision();
                Destroy(other.gameObject);
                break;
            case "InstaKill":
                _hc.Instakill();
                break;
            case "Vida":
                if (_hc.currentLifes <= 4)
                {
                    _hc.GainHealth(1);
                }
                //else
                //{
                    //Pontos.QtdPts += 11;
                //}
                Destroy(other.gameObject);
                break;
        }
    }

    private void ActivateSpeedBoost()
    {
        speedBoost = true;
        count_M = 1.5f;
        audioManager.Play("Speed");
    }

    private void ActivateShield()
    {
        hasShield = true;
        count_e = 5f;
    }

    private void HandleVictory()
    {
        Time.timeScale = 0f;
        PauseButton.SetActive(false);
        audioManager.Play("Vitoria");
        gameManager.Vitoria();
    }

    private void HandleItemPickup()
    {
        if (_hc.currentLifes <= 2)
        {
            _hc.GainHealth(1);
        }
        else
        {
            Pontos.QtdPts += 2;
        }
        audioManager.Play("Item");
    }

    private void HandleObstacleCollision()
    {
        if (hasShield)
        {
            hasShield = false;
            escudo.SetActive(false);
        }
        else
        {
            _hc.TakeDamage(1);
            df.tookDamage = true;
            audioManager.Play("PlayerHurt");

            speedDown = true;
            count_D = 1f;
            animator.SetTrigger("Dano");
        }
    }
}