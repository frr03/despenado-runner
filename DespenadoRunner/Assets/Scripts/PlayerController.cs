using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private DamageFeedback df;
    [SerializeField] private HealthController _hc;
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pontuador"))
        {
            Pontos.QtdPts += 2;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Limite"))
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().Play("Vitoria");
            gameManager.Vitoria();
        }

        if (other.CompareTag("Item"))
        {
            if (_hc.currentLifes <= 2)
            {
                _hc.GainHealth(1);
                FindObjectOfType<AudioManager>().Play("Item");
                Destroy(other.gameObject);
            }

            else
            {
                Pontos.QtdPts += 2;
                FindObjectOfType<AudioManager>().Play("Item");
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Obstaculo"))
        {
            _hc.TakeDamage(1);
            df.tookDamage = true;
            FindObjectOfType<AudioManager>().Play("PlayerHurt");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("InstaKill"))
        {
            _hc.Instakill();
        }
    }
}
