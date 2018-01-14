using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class FallingObjectBehavior : MonoBehaviour {

    [SerializeField] private float xMin = 0, xMax = 0, y = 0, z = 0;
    float curX;
    public IObjectInactiveListener objectInactiveListener;
    
    void OnEnable()
    {
        Transform t = gameObject.transform;
        curX = Random.Range(xMin, xMax);
        Vector3 pos = new Vector3(curX, y, z);
        t.SetPositionAndRotation(pos, t.rotation);
    }

    void OnDisable()
    {
        if (objectInactiveListener != null)
            objectInactiveListener.OnObjectInactive(gameObject);
    }

    // Update is called once per frame
    void LateUpdate () {
        Vector3 l = transform.localPosition;
        l.x += curX;
        transform.localPosition = l;
        transform.Rotate(new Vector3(0, 90, 0));
	}
}
