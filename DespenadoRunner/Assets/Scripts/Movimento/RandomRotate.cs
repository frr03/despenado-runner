using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    Vector3 rotate;

    void Start()
    {
        rotate = new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
    }

    void Update()
    {
        transform.Rotate(rotate * Time.deltaTime);
    }
}
