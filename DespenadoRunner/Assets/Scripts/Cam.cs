using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform target; //objeto que a câmera vai seguir
    private Vector3 offset; //posicionando a câmera atrás do objeto
    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = newPosition;
    }
}