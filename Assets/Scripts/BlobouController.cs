using System;
using System.Collections;
using UnityEngine;

public class BlobouController : MonoBehaviour {
	[SerializeField] ParticleSystem joy;
	[SerializeField] Transform happyModel;
	[SerializeField] Transform sadModel;

	void Start () {
		ScoreManager.instance.onScoreChange += (score, total) => {
			MakeHappy();
			if(score != total) {
				StartCoroutine(CallAfter(MakeSad, 2.5f));
			}
		}; 
	}

	void MakeHappy() {
		joy.Play();
		happyModel.gameObject.SetActive(true);
		sadModel.gameObject.SetActive(false);
	}

	void MakeSad() {
		happyModel.gameObject.SetActive(false);
		sadModel.gameObject.SetActive(true);
	}

	IEnumerator CallAfter(Action action, float delay) {
		yield return new WaitForSeconds(delay);
		action();
	}
}
