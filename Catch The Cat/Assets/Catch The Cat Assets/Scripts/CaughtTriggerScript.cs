using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class CaughtTriggerScript : MonoBehaviour {

    public IFallingObjectCaughtListener fallingObjectCaughtListener;

    public string fallingObjectName;

    // Use this for initialization
    public void OnTriggerEnter(Collider other)
    {
        if (fallingObjectCaughtListener != null && other.gameObject.name.Contains(fallingObjectName))
            fallingObjectCaughtListener.OnCaughtFallingObject(gameObject);
        
    }
}
