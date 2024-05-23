using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DamageFeedback df;
    [SerializeField] private HealthController _hc;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MovimentoLateral ml;

    public GameObject PauseButton;

    // PowerUp Milho
    private bool speedBoost = false;
    public float count_M;
    public float milho;

    // PowerUp Escudo
    public float count_e;
    public float res;
    public GameObject escudo;

    // Perder Velocidade ao tomar Dano
    private bool speedDown = false;
    public float count_D;
    public float getHit;

    void Update()
    {
        // Velocidades
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

        #region // ESCUDO
        
        if (res == 1)
        {
            escudo.SetActive(true);
            if (count_e <= 0)
            {
                FindObjectOfType<AudioManager>().Play("ShieldDestroy");
                res = 0;
                count_e = -1f;
            }
            count_e -= Time.deltaTime;
        }
        else if (res == 0)
        {
            escudo.SetActive(false);
        }

        #endregion

        #region // MILHO
        if (milho == 1)
        {
            speedBoost = true;
            count_M -= Time.deltaTime;
            if (count_M <= 0)
            {
                milho = 0;
                speedBoost = false;
                count_M = 0;
            }
        }
        #endregion

        #region // DIMINUIR VELOCIDADE AO TOMAR DANO
        if (getHit == 1)
        {
            speedDown = true;
            count_D -= Time.deltaTime;
            if (count_D <= 0)
            {
                getHit = 0;
                speedDown = false;
                count_D = 0;
            }
        }
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Milho"))
        {
            milho = 1;
            count_M = 1.5f;
            FindObjectOfType<AudioManager>().Play("Speed");
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("Egg"))
        {
            res = 1;
            count_e = 5f;
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("Pontuador"))
        {
            Pontos.QtdPts += 2;
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("Limite"))
        {
            Time.timeScale = 0f;
            PauseButton.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Vitoria");
            gameManager.Vitoria();
        }
        
        if (other.CompareTag("Item"))
        {
            if (_hc.currentLifes <= 2)
            {
                _hc.GainHealth(1);
            }
            else
            {
                Pontos.QtdPts += 2;
            }
            FindObjectOfType<AudioManager>().Play("Item");
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("Obstaculo"))
        {
            if (res == 1)
            {
                res = 0;
                Destroy(other.gameObject);
            }

            else if (res == 0)
            {
                _hc.TakeDamage(1);
                df.tookDamage = true;
                FindObjectOfType<AudioManager>().Play("PlayerHurt");
                Destroy(other.gameObject);

                getHit = 1;
                count_D = 1f;
            }
        }
        
        if (other.CompareTag("InstaKill"))
        {
            _hc.Instakill();
        }
    }
}