using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour {

	Transform player;
	SteamVR_TrackedController controller;
	public LineRenderer line;

	void Start () {
		player = SteamVR_Render.Top().origin;
		controller = GetComponent<SteamVR_TrackedController>();
		controller.PadClicked += new ClickedEventHandler((_1, _2) => DisplayRay());
		controller.PadUnclicked += new ClickedEventHandler((_1, _2) => HideRay());
		controller.TriggerClicked += new ClickedEventHandler((_1, _2) => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
	}

	void DisplayRay() {
		line.enabled = true;
	}

	void HideRay() {
		line.enabled = false;
		Move();
	}

	void Move() {
		Plane plane = new Plane(Vector3.up, -player.position.y);
		Ray ray = new Ray(this.transform.position, transform.forward);
		var dist = 0f;
		var hasGroundTarget = plane.Raycast(ray, out dist);
		if(hasGroundTarget) {
			Vector3 headPosOnGround = new Vector3(SteamVR_Render.Top().head.localPosition.x, 0.0f, SteamVR_Render.Top().head.localPosition.z);
			player.position = ray.origin + ray.direction * dist - new Vector3(player.GetChild(0).localPosition.x, 0f, player.GetChild(0).localPosition.z) - headPosOnGround;
		}
	}
}
