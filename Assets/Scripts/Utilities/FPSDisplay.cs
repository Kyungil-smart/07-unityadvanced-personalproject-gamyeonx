using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private float _deltaTime;

    void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 40; 
        style.normal.textColor = Color.white;

        float fps = 1.0f / _deltaTime;
        GUI.Label(new Rect(10, 10, 400, 100), $"FPS: {Mathf.Ceil(fps)}", style);
    }
}