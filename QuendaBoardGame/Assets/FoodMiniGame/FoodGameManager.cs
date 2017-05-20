using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGameManager : MonoBehaviour {
    public float barDisplay;
    private Vector2 pos;
    private Vector2 size;
    public Texture2D progressBarEmpty;
    public Texture2D progressBarFull;
    private float currentSize;
    public float current;
    public float maxSize;

    private void Start()
    {
        pos = new Vector2(0, 0);
        size = new Vector2(Screen.width, 50);
    }

    void OnGUI()
    {

        // draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), progressBarEmpty);

        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * currentSize, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), progressBarFull);
        GUI.EndGroup();

        GUI.EndGroup();

    }

    void Update()
    {
        // for this example, the bar display is linked to the current time,
        // however you would set this value based on your desired display
        // eg, the loading progress, the player's health, or whatever.
        currentSize = current / maxSize;

    }
}
