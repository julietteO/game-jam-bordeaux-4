using UnityEngine;
using UnityEngine.Events;

public class ControllerTrigger : MonoBehaviour {

	public UnityEvent OnControllerEnter;
	
	void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject.name);
		if(other.gameObject.name.Contains("Controller")) {
			OnControllerEnter.Invoke();
		}
	}
}
