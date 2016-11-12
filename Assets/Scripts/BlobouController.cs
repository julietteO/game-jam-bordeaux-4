using UnityEngine;
using UnityEngine;

public class BlobouController : MonoBehaviour {
	[SerializeField] ParticleSystem joy;

	void Start () {
		ScoreManager.instance.onScoreChange += (score, total) => joy.Play(); 
	}
}
