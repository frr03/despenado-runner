using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilhoAcougueiro : MonoBehaviour
{
    public GameObject[] HitBoxes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (HitBoxes.Length > 0)
            {
                HitBoxes[0].SetActive(true);

                if (HitBoxes.Length > 1)
                {
                    HitBoxes[1].SetActive(true);
                }

                Destroy(gameObject);
            }
        }
    }
}
