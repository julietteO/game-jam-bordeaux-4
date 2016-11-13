using UnityEngine;

public class MaterialSwapper : MonoBehaviour {
	public Material swap;

	public void Swap() {
		var tmp = GetComponent<MeshRenderer>().material;
		GetComponent<MeshRenderer>().material = swap;
		swap = tmp;
	}
}
