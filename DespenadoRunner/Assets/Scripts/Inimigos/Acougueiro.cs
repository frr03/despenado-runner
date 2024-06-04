using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acougueiro : MonoBehaviour
{
    public Transform player;
    public float speed;

    void Update()
    {
        if (player != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = player.position.x;
            newPosition.z += speed * Time.deltaTime;
            transform.position = newPosition;
        }
        else
        {
            return;
        }
    }
}