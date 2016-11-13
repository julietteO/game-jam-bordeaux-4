using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour {

	SteamVR_TrackedController controller;

	void Start () {
		controller = GetComponent<SteamVR_TrackedController>();
		controller.TriggerClicked += new ClickedEventHandler((_1, _2) => BlobouTextManager.instance.nextText());
	}
}
