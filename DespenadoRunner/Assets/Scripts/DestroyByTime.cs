using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] private float destroyTimer;

    private void Start()
    {
        Invoke("DestroyObject", destroyTimer);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}