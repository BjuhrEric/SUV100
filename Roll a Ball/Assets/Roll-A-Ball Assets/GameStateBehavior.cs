using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface ImpactListener
{
    void Impact();
}

public class GameStateBehavior : MonoBehaviour, ImpactListener {

    public static bool restart = false;
    public AudioSource bgm;
    public AudioClip impact;

    public void Impact()
    {
        bgm.PlayOneShot(impact);
    }

    private void Start()
    {
        bgm.Play();
    }
    
    void Update () {
        if (restart) {
            if (bgm != null)
            {
                bgm.Stop();
                Destroy(bgm);
                bgm = null;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                string name = SceneManager.GetActiveScene().name;
                SceneManager.UnloadSceneAsync(name);
                SceneManager.LoadSceneAsync(name);
                restart = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
