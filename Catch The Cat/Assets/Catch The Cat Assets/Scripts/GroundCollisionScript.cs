using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class GroundCollisionScript : MonoBehaviour {

    public string fallingObjectName;
    public IGroundHitListener groundHitListener;

    private void OnTriggerEnter(Collider other)
    {
        if (groundHitListener != null && other.name.Contains(fallingObjectName))
            groundHitListener.OnGroundHit(other.gameObject);
    }

}
