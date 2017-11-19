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
    public Text quit;
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
        impactListener = GameObject.Find("GameLogic").GetComponent<GameStateBehavior>(); // TODO: This should preferrably be assigned from elsewhere
    }

    // Update is called once per frame
    void Update()
    {
        System.DateTime curTime = System.DateTime.Now;
        count.text = "Count: " + points;
        if (!GameStateBehavior.restart)
            time.text = (curTime - startTime) + "";  // TODO: Text assignment should not really be managed in this class.

        if (points == CubeSpawner.NUMBER_OF_CUBES)
        {
            // Game over
            points = 0;
            restart.text = "Press \"R\" to Restart";
            quit.text = "Press \"Escape\" to Quit";
            gameObject.SetActive(false);
            GameStateBehavior.restart = true;
        }
    }
}
