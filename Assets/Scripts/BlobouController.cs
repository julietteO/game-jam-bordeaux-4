using System;
using System.Collections;
using UnityEngine;

public class BlobouController : MonoBehaviour {
	[SerializeField] ParticleSystem joy;
	[SerializeField] Transform happyModel;
	[SerializeField] Transform sadModel;

	float progress = 0f;

	void Start () {
		ScoreManager.instance.onScoreChange += (score, total) => {
			MakeHappy();
			progress = score / (float) total;
			StartCoroutine(CallAfter(MakeSadIfTheLevelIsNotFinished, 2.5f));
		}; 
	}

	void MakeHappy() {
		joy.Play();
		happyModel.gameObject.SetActive(true);
		sadModel.gameObject.SetActive(false);
	}

	void MakeSadIfTheLevelIsNotFinished() {
		if(progress < 1f) {
			happyModel.gameObject.SetActive(false);
			sadModel.gameObject.SetActive(true);
		}
	}

	IEnumerator CallAfter(Action action, float delay) {
		yield return new WaitForSeconds(delay);
		action();
	}
}
