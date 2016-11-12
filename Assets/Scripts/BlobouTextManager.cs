using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlobouTextManager : MonoBehaviour {

	int textIndex = 0;

	Text textComponent;

	string[] texts = new string[] {
		"C'est bien toi, tu es le Peintre ! ",
		"Tu peux m'aider ?"
	};


	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text> ();	
		textComponent.text = texts [textIndex];	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void nextText() {
		textIndex++;
		textComponent.text = texts [textIndex];
	}
}
