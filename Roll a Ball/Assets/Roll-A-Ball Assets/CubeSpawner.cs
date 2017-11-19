using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {

    public const int NUMBER_OF_CUBES = 15;

	// Use this for initialization
	void Start () {
        GameObject original = GameObject.Find("pointyball");

        // Figure out layout of grid
        int greatestDenominator = 1;
        for (int i = (int) Math.Sqrt(NUMBER_OF_CUBES); i > 0; i--)
        {
            if (NUMBER_OF_CUBES % i == 0)
            {
                greatestDenominator = i;
                break;
            }
        }

        // Spawn cubes
        for (int i = 0; i < greatestDenominator; i++)
        {
            for (int j = 0; j < NUMBER_OF_CUBES / greatestDenominator; j++)
            {
                Vector3 position = original.transform.position - new Vector3(2 * (i - greatestDenominator / 2), 0, 2 * (j - NUMBER_OF_CUBES / (2 * greatestDenominator)));
                GameObject cube = (GameObject)Instantiate(original, position, transform.rotation);
                cube.name = "pointyball";

                cube.AddComponent<BoxCollider>();
                BoxCollider collider = cube.GetComponent<BoxCollider>();
                collider.isTrigger = true;
                collider.size = new Vector3(1, 1, 1);

                cube.AddComponent<Rotator>();
                cube.GetComponent<Rotator>().speed = 25f;
            }
        }

        Destroy(original);
	}

}
