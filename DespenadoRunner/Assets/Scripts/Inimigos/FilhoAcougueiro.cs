using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilhoAcougueiro : MonoBehaviour
{
    [SerializeField] private FilhoMov _fm;
    public GameObject FilhoA;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _fm.FilhoMovement();
        }

        Destroy(gameObject);
    }
}
