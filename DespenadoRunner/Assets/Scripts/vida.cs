using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class vida : MonoBehaviour
{
    public int addvida = 1;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            HealthController pm = other.GetComponent<HealthController>();
            pm.currentLifes += addvida;
            Destroy(gameObject);

        }
    }
}
