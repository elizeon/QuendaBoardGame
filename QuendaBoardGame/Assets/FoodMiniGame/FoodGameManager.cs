using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Manager for food minigame
 * Written by Nathan Gane
 * 
 * */

public class FoodGameManager : MonoBehaviour {
    public float barDisplay;
    private Vector2 pos;
    private Vector2 size;
    public Texture2D progressBarEmpty;
    public Texture2D progressBarFull;
    private float currentSize;
    public float currentHunger;
    public float maxHunger;
    public GameObject foodGame;
    Game m_game;
    private bool m_result;

    private void Start()
    {
        m_game = FindObjectOfType<Game>();
        pos = new Vector2(0, 0);
        size = new Vector2(Screen.width, 50);
    }

    public void ReturnToGame(int currentScene)
    {
        Destroy(foodGame);

        m_game.scene.SetActive(true);

        m_game.TriggerCamera(true);
        m_game.AllowStartMovement();

        if (m_result)
        {
            m_game.MoveOnPath(3);

            m_game.messageBox.DisplayMessageBox("Success! Move forward 3 spaces.", false);
            m_game.DisallowStartMovement();

            m_game.AddResult("Food Game", m_result);
            m_game.SaveResults();
        }
        else
        {
            m_game.MoveOnPath(-3);

            m_game.messageBox.DisplayMessageBox("You failed. Move backwards 3 spaces.", false);
            m_game.DisallowStartMovement();
            m_game.AddResult("Food Game", m_result);
            m_game.SaveResults();
        }
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

    public void Lose()
    {
        m_result = false;
        ReturnToGame(5);
    }

    public void Win()
    {
        m_result = true;
        ReturnToGame(5);
    }

    void Update()
    {

        currentHunger = FindObjectOfType<Quenda>().hunger;
        currentSize = currentHunger / maxHunger;
        if (currentHunger <= 0)
        {
            Lose();
        }
        if(currentHunger >= maxHunger)
        {
            Win();
        }
    }
}
