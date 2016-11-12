using UnityEngine;

public class SplittingMesh : MonoBehaviour {

	[SerializeField] GameObject fullForm;
	[SerializeField] GameObject splitForm;
	[SerializeField] float relativeVelocityThreshold;

	void Start () {
		fullForm.SetActive(true);
		splitForm.SetActive(false);
	}

	void Split() {
		fullForm.SetActive(false);
		splitForm.SetActive(true);
		splitForm.transform.SetParent(fullForm.transform.parent);
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.rigidbody == null) 
			return;

		Debug.Log(collision.transform.name+" - "+collision.rigidbody.velocity.magnitude);
		if(collision.rigidbody.velocity.magnitude > relativeVelocityThreshold) {
			Split();
		}
	}
}
