using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float count;

    private IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5, 10, 100, 25), "FPS: " + Mathf.Round(count));
    }

}
