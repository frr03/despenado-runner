using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoLateral : MonoBehaviour
{
    #region Variaveis
    [SerializeField] private Animator animator;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask groundMask;

    private BoxCollider boxCollider;
    private CapsuleCollider capsuleCollider;
    private SphereCollider sphereCollider;

    private int pista = 1;
    [SerializeField] private float velocidade; // velocidade player
    public float distancia; // distancia entre pistas
    public float[] lanes = new float[3] { -3f, 0f, 3f }; // pistas
    public float pulo = 400f; // forca pulo
    private float currentTime = 0f; // timer
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTime;
    private float length;

    private bool pisandoNoChao; // player no chao
    private bool rasteira; // rasteira
    private bool isTimerRunning = false; // timer rasteira
    private bool isMoving;

    private float jumpCooldown = 1.5f; // cooldown duration
    private float lastJumpTime = -1.5f; // tracks the last jump time

    #endregion

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (Time.timeScale <= 0)
        {
            return;
        }

        if (isMoving)
        {
            MoveToTarget();
        }

        HandleSwipes();
        HandleTaps();
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime); // Constant forward movement
    }

    private void MoveToTarget()
    {
        float covered = (Time.time - startTime) * velocidade;
        float journey = covered / length;
        transform.position = Vector3.Lerp(startPosition, endPosition + Vector3.forward * (velocidade / 3), journey);

        if (journey >= 1f)
        {
            isMoving = false;
        }
    }

    private void HandleSwipes()
    {
        if (SwipeManager.swipeLeft) MoveLeft();
        if (SwipeManager.swipeRight) MoveRight();
        if (SwipeManager.swipeUp && pisandoNoChao && !rasteira) Jump();
        if (SwipeManager.swipeDown && pisandoNoChao && !rasteira) StartSlide();
        if (isTimerRunning) UpdateTimer();
    }

    private void HandleTaps()
    {
        // Handle taps if needed
    }

    private void CalculateMovement()
    {
        isMoving = true;
        startPosition = transform.position;
        endPosition = new Vector3(lanes[pista], transform.position.y, transform.position.z);
        startTime = Time.time;
        length = Vector3.Distance(startPosition, endPosition);
    }

    private void MoveLeft()
    {
        if (!isMoving && pista > 0)
        {
            pista--;
            CalculateMovement();
        }
    }

    private void MoveRight()
    {
        if (!isMoving && pista < lanes.Length - 1)
        {
            pista++;
            CalculateMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chao"))
        {
            pisandoNoChao = true;
        }
    }

    private void Jump()
    {
        if (Time.time >= lastJumpTime + jumpCooldown)
        {
            float altura = GetComponent<Collider>().bounds.size.y;
            bool noChao = Physics.Raycast(transform.position, Vector3.down, (altura / 2) * 0.1f, groundMask);
            rb.AddForce(Vector3.up * pulo);
            animator.SetTrigger("Pulo");
            lastJumpTime = Time.time;
        }
    }

    private void StartSlide()
    {
        rasteira = true;
        StartTimer();
    }

    #region Timer Methods

    private void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            StopTimer();
            rasteira = false;
        }
    }

    private void StartTimer()
    {
        currentTime = 1f;
        isTimerRunning = true;

        Vector3 newSize = boxCollider.size;
        Vector3 newPosi = boxCollider.center;

        newSize.y /= 2;
        newPosi.y = -0.5f;

        boxCollider.size = newSize;
        boxCollider.center = newPosi;

        capsuleCollider.enabled = false;
        sphereCollider.enabled = true;
        animator.SetTrigger("Agachar");
    }

    private void StopTimer()
    {
        isTimerRunning = false;

        Vector3 newSize = boxCollider.size;
        Vector3 newPosi = boxCollider.center;

        newSize.y *= 2;
        newPosi.y = 0f;

        boxCollider.size = newSize;
        boxCollider.center = newPosi;

        capsuleCollider.enabled = true;
        sphereCollider.enabled = false;
    }

    #endregion

    #region Speed Methods

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
