using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float speed;

	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(speed * Time.deltaTime * new Vector3(0, 1, 0));
	}
}
