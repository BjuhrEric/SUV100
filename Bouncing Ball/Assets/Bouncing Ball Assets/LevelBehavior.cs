using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class LevelBehavior : MonoBehaviour {

    enum GameState
    {
        // In case I want to extend it with more states, such as first tutorial on first run.
        Paused, Playing, GameOver
    }

    [SerializeField] private GameObject extraLifeContainer;
    [SerializeField] private Text point_label;
    [SerializeField] private Text point_number;
    [SerializeField] private Text info;

    private GameState state;
    private float startTime;
    private float points;
    //private float extraLives;
    private LinkedList<GameObject> extraLives;

    private LinkedList<IResetListener> resetListeners = new LinkedList<IResetListener>();

	// Use this for initialization
	void Start () {
        state = GameState.Paused;
        points = 0;

        // Find all children of extraLifeContainer.
        LinkedList<GameObject> children = new LinkedList<GameObject>();
        foreach (Transform child in extraLifeContainer.transform)
        {
            GameObject obj = child.gameObject;
            obj.SetActive(true);
            children.AddLast(obj);
        }
        extraLives = children;
	}
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButton("Restart"))
            ResetGame();
        else if (Input.anyKey)
            if (state == GameState.GameOver)
                Application.Quit();
            else if (state == GameState.Paused)
                ResetRound();
            
        

        /*
         * UI updates depending on game state
         */

        if (state == GameState.Playing)
        {
            point_number.text = points.ToString();
            info.text = "";
        }

        if (state == GameState.Paused)
        {
            info.text = "Press any key to start!";
        }

        if (state == GameState.GameOver)
        {
            point_label.text = "Final score:";
            info.text = "Game over! Press any key to quit.\nPress R to restart.";
        }
	}

    private void ResetGame()
    {
        // Do stuff
        Start();
        ResetRound(); // Provides some visual feedback to the player. Has to recalculate positions twice though...
        state = GameState.Paused;
    }

    private void ResetRound()
    {
        startTime = Time.time;
        state = GameState.Playing;
        foreach (IResetListener listener in resetListeners) listener.Reset();
    }

    public bool IsRunning()
    {
        return state == GameState.Playing;
    }

    public void RegisterCollision()
    {
        if (extraLives.Count <= 0)
            state = GameState.GameOver;
        else
        {
            GameObject extraLife = extraLives.First.Value;
            extraLife.SetActive(false);
            extraLives.RemoveFirst();
            state = GameState.Paused;
        }
    }

    public void AddPoint()
    {
        points++;
    }

    public void AddResetListener(IResetListener listener)
    {
        resetListeners.AddLast(listener);
    }


}
