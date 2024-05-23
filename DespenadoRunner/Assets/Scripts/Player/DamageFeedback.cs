using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;
    List<Color> origColors = new List<Color>(); // List to store original colors
    float flashTime = 0.15f; // Flash duration

    public bool tookDamage = false;

    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        // Store the original colors of all materials
        foreach (var mat in skinnedMeshRenderer.materials)
        {
            origColors.Add(mat.color);
        }
    }

    void Update()
    {
        if (tookDamage)
        {
            StartCoroutine(EFlash());
        }
    }

    void FlashStart()
    {
        // Set all materials to red
        foreach (var mat in skinnedMeshRenderer.materials)
        {
            mat.color = Color.red;
        }
        Invoke("FlashStop", flashTime);
    }

    void FlashStop()
    {
        // Restore original colors
        for (int i = 0; i < skinnedMeshRenderer.materials.Length; i++)
        {
            skinnedMeshRenderer.materials[i].color = origColors[i];
        }
    }

    IEnumerator EFlash()
    {
        // Set all materials to red
        foreach (var mat in skinnedMeshRenderer.materials)
        {
            mat.color = Color.red;
        }

        yield return new WaitForSeconds(flashTime);

        // Restore original colors
        for (int i = 0; i < skinnedMeshRenderer.materials.Length; i++)
        {
            skinnedMeshRenderer.materials[i].color = origColors[i];
        }

        tookDamage = false;
    }
}