using System;
using UnityEngine;
using UnityEngine.UI;

public class SplittingMesh : MonoBehaviour {

	[SerializeField] GameObject fullForm;
	[SerializeField] GameObject splitForm;
	[SerializeField] float relativeVelocityThreshold;
	[SerializeField] Text debugText;

	public event Action onSplit;

	void Start () {
		fullForm.SetActive(true);
		splitForm.SetActive(false);
	}

	public void Split() {
		fullForm.SetActive(false);
		splitForm.SetActive(true);
		splitForm.transform.SetParent(fullForm.transform.parent, false);
		splitForm.transform.localPosition = fullForm.transform.localPosition;
		splitForm.transform.localRotation = fullForm.transform.localRotation;
		if(onSplit != null) {
			onSplit();
		} 
	}

	public void UnSplit() {
		fullForm.SetActive(true);
		splitForm.SetActive(false);
	}
	
	void OnCollisionEnter(Collision collision) {
		if(collision.rigidbody == null) 
			return;

		if(debugText != null) {
			debugText.text = collision.transform.name+" - "+collision.impulse.magnitude;
		}

		if(collision.impulse.magnitude > relativeVelocityThreshold) {
			Split();
		}
	}
}
