using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoLateral : MonoBehaviour
{
    #region //Vari�veis

    [SerializeField] Rigidbody RB; //fazendo contato com o rigidbody
    [SerializeField] LayerMask groundMask; //fazendo contato com a layermask
    BoxCollider BC;
    CapsuleCollider CC;
    SphereCollider SC;

    private int pista = 1; //posicao inicial | pista 0 = pista direita | pista 1 = pista meio | pista 2 = pista esquerda
    [SerializeField] private float velocidade; //velocidade do player
    public float distancia; //distancia entre pistas
    public float[] lanes = new float[3] { -3f, 0f, 3f }; //coordenadas das pistas
    public float Pulo = 400f; //for�a do pulo
    private float currentTime = 0f; //temporizador
    Vector3 _startPoition;
    Vector3 _endPosition;
    private float _startTime;
    private float _Legnth;

    private bool pisandoNoChao; //variavel para verificar se o Player est� pisando no ch�o
    private bool rasteira; //variavel para verificar se o Player est� dando uma rasteira
    private bool isTimerRunning = false; //variavel para verificar se o temporizador da rasteira est� ativo
    private bool _isMoving;

    #endregion

    void Start()
    {
        BC = GetComponent<BoxCollider>();
        CC = GetComponent<CapsuleCollider>();
        SC = GetComponent<SphereCollider>();
    }

    void Update()
    {
        //Impede player de mover ap�s perder
        if (Time.timeScale <= 0)
        {
            return;
        }

        if (_isMoving)
        {
            float Covered = (Time.time - _startTime) * velocidade;

            float Journey = Covered / _Legnth;
            transform.position = Vector3.Lerp(_startPoition, _endPosition + Vector3.forward * (velocidade / 3), Journey);

            if (Journey >= 1f)
            {
                _isMoving = false;
            }
        }

        #region //Swipes

        //movimento para a esquerda
        if (SwipeManager.swipeLeft)
        {
            /*pista--;
            if (pista == -1)
            {
                pista = 0;
            }
            transform.position += Vector3.left * 3;

            float newX = lanes[pista];
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);*/

            MoveLeft();
        }

        //movimento para a direita
        if (SwipeManager.swipeRight)
        {
            /*pista++;
            if (pista == 3)
            {
                pista = 2;
            }
            transform.position += Vector3.right * 3;

            float newX = lanes[pista];
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);*/

            MoveRight();
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

    private void CalculateMovement()
    {
        _isMoving = true;
        _startPoition = transform.position;
        _endPosition = new Vector3(lanes[pista], transform.position.y, transform.position.z);
        _startTime = Time.time;
        _Legnth = Vector3.Distance(_startPoition, _endPosition);
    }

    private void MoveLeft()
    {
        if (_isMoving == false)
        {
            pista--;
            if(pista == -1)
            {
                pista = 0;
            }
        }
        CalculateMovement();
    }

    private void MoveRight()
    {
        if (_isMoving == false)
        {
            pista++;
            if (pista == 3)
            {
                pista = 2;
            }
        }
        CalculateMovement();
    }

    //verificando se o player est� no ch�o
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chao"))
        {
            pisandoNoChao = true;
        }
    }

    //m�todo pulo
    void Jump()
    {
        float altura = GetComponent<Collider>().bounds.size.y;
        bool NoChao = Physics.Raycast(transform.position, Vector3.down, (altura / 2) * 0.1f, groundMask);
        RB.AddForce(Vector3.up * Pulo);
    }

    #region //M�todos temporizador/rasteira

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

            Debug.Log("em p�");
        }
    }

    #endregion

    #region // Velocidades
    public void VelocidadeNormal()
    {
        velocidade = 9;
    }

    public void VelocidadeBoost()
    {
        velocidade = 12;
    }

    public void VelocidadeDown()
    {
        velocidade = 6;
    }
    #endregion
}