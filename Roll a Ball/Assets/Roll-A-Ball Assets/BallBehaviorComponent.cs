using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBehaviorComponent : MonoBehaviour
{

    private int points;
    public Text count;
    public Text restart;
    public Text time;
    public ImpactListener impactListener;
    public System.DateTime startTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("pointyball"))
        {
            other.gameObject.SetActive(false);
            points++;
            impactListener.Impact();
        }
    }

    // Use this for initialization
    void Start()
    {
        startTime = System.DateTime.Now;
        points = 0;
        impactListener = GameObject.Find("GameLogic").GetComponent<GameStateBehavior>(); // This should preferrably be assigned from elsewhere
    }

    // Update is called once per frame
    void Update()
    {
        System.DateTime curTime = System.DateTime.Now;
        count.text = "Count: " + points;
        if (!GameStateBehavior.restart)
            time.text = (curTime - startTime) + "";

        if (points == CubeSpawner.NUMBER_OF_CUBES)
        {
            // Game over
            points = 0;
            restart.text = "Press \"R\" to Restart";
            gameObject.SetActive(false);
            GameStateBehavior.restart = true;
        }
    }
}
