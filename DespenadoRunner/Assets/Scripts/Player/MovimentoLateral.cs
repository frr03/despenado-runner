using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoLateral : MonoBehaviour
{
    #region //Variáveis

    [SerializeField] Rigidbody RB; //fazendo contato com o rigidbody
    [SerializeField] LayerMask groundMask; //fazendo contato com a layermask
    BoxCollider BC;
    CapsuleCollider CC;
    SphereCollider SC;

    private int pista = 1; //posicao inicial | pista 0 = pista direita | pista 1 = pista meio | pista 2 = pista esquerda
    [SerializeField] private float velocidade = 5; //velocidade do player
    public float distancia = 3; //distancia entre pistas
    public float[] lanes = new float[3] { -3f, 0f, 3f }; //coordenadas das pistas
    public float Pulo = 400f; //força do pulo
    private float currentTime = 0f; //temporizador

    private bool pisandoNoChao; //variavel para verificar se o Player está pisando no chão
    private bool rasteira; //variavel para verificar se o Player está dando uma rasteira
    private bool isTimerRunning = false; //variavel para verificar se o temporizador da rasteira está ativo

    #endregion

    void Start()
    {
        BC = GetComponent<BoxCollider>();
        CC = GetComponent<CapsuleCollider>();
        SC = GetComponent<SphereCollider>();
    }

    void Update()
    {
        //Impede player de mover após perder
        if (Time.timeScale <= 0)
        {
            return;
        }

        #region //Swipes

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

        //pulo
        if (SwipeManager.swipeUp && pisandoNoChao == true && rasteira == false)
        {
            Jump();
            pisandoNoChao = false;
        }

        //rasteira 1
        if (SwipeManager.swipeDown && pisandoNoChao == true && rasteira == false)
        {
            rasteira = true;
            StartTimer();
        }
        // rasteira 2
        if (isTimerRunning)
        {
            UpdateTimer();
        }

        #endregion

        #region //Taps

        //cheat para trocar de fase
        /*if (SwipeManager.tap4)
        {
            //Debug.Log("Mudou de fase");
        }*/

        #endregion

        transform.Translate(transform.forward * velocidade * Time.deltaTime); //movimento constante para a frente

        //fechar jogo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("fechou o jogo.");
        }
    }

    //verificando se o player está no chão
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chao"))
        {
            pisandoNoChao = true;
            //Debug.Log("No chão");
        }
    }

    //método pulo
    void Jump()
    {
        float altura = GetComponent<Collider>().bounds.size.y;
        bool NoChao = Physics.Raycast(transform.position, Vector3.down, (altura / 2) * 0.1f, groundMask);
        RB.AddForce(Vector3.up * Pulo);
    }

    #region //Métodos temporizador/rasteira

    //atualiza o temporizador
    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            StopTimer();
            rasteira = false;
        }
    }

    //inicializa o temporizador da rasteira
    public void StartTimer()
    {
        currentTime = 1f;
        isTimerRunning = true;

        Vector3 newSize = BC.size;
        Vector3 newPosi = BC.center;

        newSize.y = newSize.y / 2;
        newPosi.y = -0.5f;

        BC.size = newSize;
        BC.center = newPosi;

        CC.enabled = false;
        SC.enabled = true;

        Debug.Log("rasteira");
    }

    //para o temporizador da rasteira
    public void StopTimer()
    {
        isTimerRunning = false;

        if (isTimerRunning == false)
        {
            Vector3 newSize = BC.size;
            Vector3 newPosi = BC.center;

            newSize.y = newSize.y * 2;
            newPosi.y = 0f;

            BC.size = newSize;
            BC.center = newPosi;

            CC.enabled = true;
            SC.enabled = false;

            Debug.Log("em pé");
        }
    }

    #endregion
}