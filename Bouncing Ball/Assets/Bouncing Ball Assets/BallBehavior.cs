using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Ball;

public class BallBehavior : MonoBehaviour, IResetListener {

    [SerializeField] private GameObject levelBehaviorObject;
    [SerializeField] private Ball ball;

    private LevelBehavior levelBehavior;
    private Vector3 t_d = new Vector3(0, -0.5f, 0);
    private Vector3 t_r = new Vector3(0.5f, 0, 0);

    void OnCollisionEnter(Collision col)
    {
        if (levelBehavior != null)
            levelBehavior.RegisterCollision();
    }

    void OnTriggerExit(Collider col)
    {
        levelBehavior.AddPoint();
    }

    // Use this for initialization
    void Start ()
    {
        if (levelBehaviorObject != null)
        {
            levelBehavior = levelBehaviorObject.GetComponent<LevelBehavior>();
            levelBehavior.AddResetListener(this);
        }
	}

    void Update()
    {
        if (levelBehavior.IsRunning())
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        else
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Reset()
    {
        ball.ResetPosition();
    }

}
