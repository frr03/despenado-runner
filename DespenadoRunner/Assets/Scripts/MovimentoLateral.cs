using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoLateral : MonoBehaviour
{
    private int pista = 1; //posicao inicial | pista 0 = pista direita | pista 1 = pista meio | pista 2 = pista esquerda
    [SerializeField] private float velocidade = 5; //velocidade do player
    public float distancia = 3; //distancia entre pistas
    public float[] lanes = new float[3] { -3f, 0f, 3f }; //coordenadas das pistas

    void Update()
    {
        //movimento para a esquerda
        if (SwipeManager.swipeLeft)
        {
            pista--;
            if (pista == -1)
            {
                pista = 0;
            }
            transform.position += Vector3.left * 3;

            float newX = lanes[pista];
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        //movimento para a direita
        if (SwipeManager.swipeRight)
        {
            pista++;
            if (pista == 3)
            {
                pista = 2;
            }
            transform.position += Vector3.right * 3;

            float newX = lanes[pista];
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        transform.Translate(transform.forward * velocidade * Time.deltaTime); //movimento constante para a frente

        //fechar jogo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("fechou o jogo.");
        }
    }
}