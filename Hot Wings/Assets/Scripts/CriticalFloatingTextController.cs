using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalFloatingTextController : MonoBehaviour {

    private static FloatingText criticalpopupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!criticalpopupText)
            criticalpopupText = Resources.Load<FloatingText>("Prefabs/CriticalPopUpTextParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(criticalpopupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.2f, .2f), location.position.y + Random.Range(-.2f, .2f)));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}