using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour, IResetListener {

    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;
    [SerializeField] private float originX;
    [SerializeField] private float repeatX;
    [SerializeField] private float restartX;
    [SerializeField] private float velocity;
    [SerializeField] private float acceleration;
    [SerializeField] private GameObject levelBehaviorObject;

    private LevelBehavior levelBehavior;
    private Transform t;
    private float startTime;

    // Use this for initialization
    void Start () {
        t = gameObject.transform;
        RandomizeHeight();
        startTime = Time.time;

        if (levelBehaviorObject != null)
        {
            levelBehavior = levelBehaviorObject.GetComponent<LevelBehavior>();
            levelBehavior.AddResetListener(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (levelBehavior.IsRunning())
        {
            float timePassed = Time.time - startTime;
            // v = v0 + at, v = dx/dt, dx/dt = v0 + at <=> dx = dt(v0 + at). Scale unit vector for direction u-> by dx to get translation t->
            t.Translate(Vector3.left * (Time.deltaTime * (velocity + acceleration * timePassed))); 
            if (t.position.x < restartX)
            {
                Vector3 pos = t.position;
                pos.x = repeatX;
                t.SetPositionAndRotation(pos, t.rotation);
                RandomizeHeight();
            }
        }
	}

    public void RandomizeHeight()
    {
        Vector3 pos = t.position;
        pos.y = Random.Range(minHeight, maxHeight);
        t.SetPositionAndRotation(pos, t.rotation);
    }

    public void Reset()
    {
        startTime = Time.time;
        Vector3 pos = t.position;
        pos.x = originX;
        pos.y = Random.Range(minHeight, maxHeight);
        t.SetPositionAndRotation(pos, t.rotation);
    }
}
