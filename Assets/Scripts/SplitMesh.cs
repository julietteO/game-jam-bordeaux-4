using System;
using UnityEngine;

public class SplitMesh : MonoBehaviour {

	FixedJoint[] parts;
	[SerializeField] float relativeVelocityThreshold;
	
	public event Action onSplit;

	void Start () {
		parts = GetComponentsInChildren<FixedJoint>();
	}

	public void Split() {
		foreach(var part in parts) {
			Destroy(part);
		}
		
		GetComponent<Collider>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		
		if(onSplit != null) {
			onSplit();
		} 
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.rigidbody == null) 
			return;

		if(collision.impulse.magnitude > relativeVelocityThreshold) {
			Split();
		}
	}
}
