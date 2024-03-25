using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Color origColor; //Mat Original
    float flashTime = 0.15f; //Tempo do flash

    public bool tookDamage = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        origColor = meshRenderer.material.color;
    }

    void Update()
    {
        if (tookDamage == true)
        {
            StartCoroutine(EFlash());
        }
    }

    void FlashStart()
    {
        meshRenderer.material.color = Color.red;
        Invoke("FlashStop", flashTime);
    }

    void FlashStop()
    {
        meshRenderer.material.color = origColor;
    }

    IEnumerator EFlash()
    {
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashTime);
        meshRenderer.material.color = origColor;
        tookDamage = false;
    }
}