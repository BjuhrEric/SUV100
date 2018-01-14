using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class LevelBehavior : MonoBehaviour, IGroundHitListener, IFallingObjectCaughtListener {

    [SerializeField] private FallingObjectPoolScript poolScript;
    [SerializeField] private GroundCollisionScript groundCollisionScript;
    [SerializeField] private FallingObjectPoolScript fallingObjectPoolScript;
    [SerializeField] private Platformer2DUserControl userInput; 
    [SerializeField] private Text points;
    [SerializeField] private Text gameover;
    [SerializeField] private Text pressButton;
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip splatSound;
    [SerializeField] private List<AudioClip> caughtSounds;

    private List<GameObject> fallingObjects = new List<GameObject>();
    private bool ggwp;
    private bool canQuit;

    private const float t = 600f;
    private const float y = 3f, k = 0.25f, variation = 0.1f, offset = 0.25f;

    private float m;
    private float startTime;
    private int pts;
    private float fallSpeed = 1;

	// Use this for initialization
	void Start ()
    {
        // Only need to do once...
        groundCollisionScript.groundHitListener = this;
        Cursor.visible = false;
        m = (y - k) / t;

        // Might be moved to Update for input based start...
        InitGame();
    }

    private void InitGame()
    {
        startTime = Time.time;
        pts = 0;
        fallSpeed = 1;
        points.text = "Points: 0";
        gameover.text = "";
        pressButton.text = "";
        bgmSource.Stop(); // If it was already playing.
        bgmSource.Play();

        ggwp = false;
        canQuit = false;
        userInput.ggwp = false;
        SpawnFallingObject();
    }

    public void SpawnFallingObject()
    {
        if (ggwp)
            return;

        if (poolScript.HasAvailableObject())
        {
            GameObject obj = poolScript.PullObject();
            Animator animator = obj.GetComponent<Animator>();
            obj.GetComponent<CaughtTriggerScript>().fallingObjectCaughtListener = this;

            animator.StartPlayback();
            obj.SetActive(true);
            animator.SetFloat("FallSpeed", fallSpeed);
            fallingObjects.Add(obj);

            float t = Time.time - startTime;
            float y = k + m * t;
            float randomDropRate = Random.Range(1 - variation, 1 + variation) * Random.Range(y, y + offset);
            float nextDropIn = 1f / randomDropRate;
            Invoke("SpawnFallingObject", nextDropIn);
        }
        else
        {
            Invoke("SpawnFallingObject", 0.01f); //wait 10 ms then try again
        }
    }

    public void OnGroundHit(GameObject fallingObject)
    {
        fxSource.PlayOneShot(splatSound);

        foreach (GameObject obj in fallingObjects)
        {
            Animator animator = obj.GetComponent<Animator>();
            animator.StopPlayback();
            obj.SetActive(false);
        }
        fallingObjects.Clear();

        ggwp = true;
        gameover.text = "Game Over";
        Invoke("allowQuit", 1f);

        if (userInput != null)
            userInput.ggwp = true;
    }

    private void allowQuit()
    {
        canQuit = true;
        pressButton.text = "Press Any Button To Quit\nPress R To Restart";
    }

    public void Update()
    {
        if (!ggwp)
        {
            fallSpeed += 0.005f * Time.deltaTime;
        }
        else if (canQuit)
        {
            if (Input.GetKey("r"))
            {
                // Restart.
                InitGame();
            }
            else if (Input.anyKey)
            {
                QuitGame();
            }
        }
    }

    private void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
    #else
        Application.Quit();
    #endif
    }

    public void OnCaughtFallingObject(GameObject fallingObject)
    {
        //fallingObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        Animator animator = fallingObject.GetComponent<Animator>();
        //animator.SetBool("Falling", false);
        animator.StopPlayback();
        fallingObject.SetActive(false);
        pts++;
        if (points != null)
            points.text = "Points: " + pts;
        fxSource.PlayOneShot(caughtSounds[Random.Range(0, caughtSounds.Count)]);
        fallingObjects.Remove(fallingObject);
    }
}
