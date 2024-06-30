using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilhoMov : MonoBehaviour
{
    public GameObject Filho;

    public Vector3 posicaoFinal;

    public float moveDuration = 1.0f;

    public void FilhoMovement()
    {
        StartCoroutine(MovimentoFilho(Filho, posicaoFinal, moveDuration));
        FindObjectOfType<AudioManager>().Play("FilhoRun");
    }

    private IEnumerator MovimentoFilho(GameObject obj, Vector3 targetPos, float duration)
    {
        float startTime = Time.time;

        Vector3 initialPosition = obj.transform.position;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;

            obj.transform.position = Vector3.Lerp(initialPosition, targetPos, t);

            yield return null;
        }

        obj.transform.position = targetPos;
    }
}