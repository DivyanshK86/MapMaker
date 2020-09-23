using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckManager : MonoBehaviour {

	public List<GameObject> colliderObjects;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == 8)
			colliderObjects.Add(other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.layer == 8)
			colliderObjects.Remove(other.gameObject);
	}

	public bool IsOnGround()
	{
		return colliderObjects.Count > 0;
	}
}
