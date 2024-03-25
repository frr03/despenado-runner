using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pontos : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public static int QtdPts;

    void Start()
    {
        QtdPts = 0;
    }

    void Update()
    {
        scoreText.text = QtdPts.ToString();
    }
}
