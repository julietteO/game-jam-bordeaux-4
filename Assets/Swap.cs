using UnityEngine;
using System.Collections;

public class Swap : MonoBehaviour {
	public void DoSwap() {
		GetComponent<Light>().enabled = !GetComponent<Light>().enabled; 
	}
}
