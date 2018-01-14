using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class FallingObjectPoolScript : MonoBehaviour, IObjectInactiveListener {

    [SerializeField] private GameObject objectType;
    [SerializeField] private int initialAmount = 10;
    private readonly LinkedList<GameObject> fallingObjects = new LinkedList<GameObject>();

	// Use this for initialization
	void Start () {
        fallingObjects.AddFirst(objectType);
        objectType.SetActive(false);
        objectType.GetComponent<FallingObjectBehavior>().objectInactiveListener = this;

        for (int i = 0; i < initialAmount-1; i++) // We already have one. Create i-1 new ones.
        {
            GameObject obj = (GameObject) Instantiate(objectType, gameObject.transform);
            obj.SetActive(false);
            obj.GetComponent<FallingObjectBehavior>().objectInactiveListener = this;
            fallingObjects.AddLast(obj);
        }
	}
	
	public bool HasAvailableObject()
    {
        return fallingObjects.Count > 0;
    }

    public GameObject PullObject()
    {
        GameObject obj = fallingObjects.First.Value;
        fallingObjects.RemoveFirst();
        return obj;
    }

    public void OnObjectInactive(GameObject obj)
    {
        fallingObjects.AddLast(obj);
    }
}
